using UnityEngine;

public class BombBaseScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab; // 爆発エフェクトのプレハブ
    [SerializeField] private float explosionRadius = 5.0f; // 爆発の半径
    [SerializeField] private float explosionDamage = 1f; // 爆発のダメージ
    [SerializeField] private float fuseTime = 3.0f; // 導火線の時間
    [SerializeField] private float fuseTimeDelay = 0.5f;// 爆発までのランダムな遅延時間
    [SerializeField] private AudioClip explosionSound; // 爆発音
    

    [SerializeField] private AudioClip fuseSound;
   
    private Rigidbody2D rb; // ボムのRigidbody2Dコンポーネント
    private AudioSource audioSource; // オーディオソースコンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // 導火線の音を再生
        fuseTime += Random.Range(-fuseTimeDelay, fuseTimeDelay);// 爆発までの時間にランダムな遅延を追加
        Invoke("Explode", fuseTime); // 指定した時間後に爆発を呼び出す
    }

    // ボムの初期化メソッド
   public void BombInitialize(Vector2 direction, float power)
    {
        // ボムの初期化処理
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        // 爆発エフェクトの生成
        if(explosionEffectPrefab) Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        // 爆発音の再生
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        // 半径内のオブジェクトにダメージを与える
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Debug.DrawRay(transform.position, Vector3.up * explosionRadius, Color.red, 2.0f);
        foreach (var hitCollider in hitColliders)
        {
            if(!hitCollider) continue; // 安全チェック
            // ダメージを与える処理（例: プレイヤーや敵にダメージを与える）
            HealthManager healthManager = hitCollider.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(explosionDamage);
            }
        }
        // ボムオブジェクトの削除
        Destroy(gameObject);
    }
}
