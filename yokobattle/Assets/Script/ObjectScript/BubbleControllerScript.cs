using UnityEngine;

public class BubbleControllerScript : BulletController
{

    [SerializeField] private GameObject popEffectPrefab; // バブルが弾けるエフェクトのプレハブ
    [SerializeField] float BuoyancyRange = 2f; // 浮力が働く範囲
    [SerializeField] float dampingForceRange = 0.5f; // 減衰力の範囲
    protected float buoyancyForce = 2f; // 浮力の強さ
    protected float dampingForce = 0.2f; // 減衰力の強さ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // バブルの初期化処理
       TestStart();
        buoyancyForce =Random.Range(-BuoyancyRange * 0.5f, BuoyancyRange);// 浮力の強さをランダムに設定
        dampingForce = Random.Range(0.1f, dampingForceRange); // 減衰力の強さをランダムに設定
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // バブルの発射処理
    private void TestStart()
    {
       // BulletInitialize(new Vector2(1,0)); // 右方向に発射
    }
    private void OnEnable()
    {
        // バブルの寿命を設定
        Invoke("DestroyBubble", lifetime);
    }

    private void OnDisable()
    {
        // バブルが無効化されたときに呼ばれる
        CancelInvoke("DestroyBubble");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーに衝突した場合
        if (collision.CompareTag("Player"))
        {
            // プレイヤーにダメージを与える
            var playerHealth = collision.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(defaultDamage);
            }
            // バブルを破壊
            DestroyBubble();
        }
    }

    private void DestroyBubble()
    {
        // バブルが弾けるエフェクトを生成
        if (popEffectPrefab != null)
        {
            Instantiate(popEffectPrefab, transform.position, Quaternion.identity);
        }
        // バブルを破壊
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        // バブルの移動処理
        if (rb != null)
        {
            rb.linearDamping += dampingForce; // 徐々に減速させる
            rb.AddForce(new Vector2(0, buoyancyForce)); // 浮力を加える
            if(rb.linearVelocity.x < 0.1f)
            {
            //    rb.gravityScale = 1f; // 水平速度が遅くなったら重力をかける
              //  buoyancyForce = 0f; // 浮力を無効化
            }
        }
    }
}
