using UnityEngine;

public class BubbleBombController : BulletController
{
    [SerializeField] private int spawnCount = 5; // 発生させるミニバブルの数
    [SerializeField] private GameObject miniBubblePrefab; // ミニバブルのプレハブ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<HealthManager>()?.TakeDamage(defaultDamage); // プレイヤーにダメージを与える
            Destroy(gameObject); // バブルボムを破壊
        }

        // ミニバブルを発生させる
        for (int i = 0; i < spawnCount; i++)
        {
            // ミニバブルをランダムな方向に発生させる
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnOffset = new Vector3 (0, 0.5f, 0); // 少し上にオフセットして生成
            GameObject miniBubble = Instantiate(miniBubblePrefab, this.transform.position + spawnOffset, Quaternion.identity);
            BulletController miniBubbleController = miniBubble.GetComponentInChildren<BulletController>();
            if (miniBubbleController != null)
            {

                miniBubbleController.BulletInitialize(randomDirection);

            }
            else
            {
                Debug.LogWarning("BulletController component not found on miniBubblePrefab.");
            }
        }
        Destroy(gameObject); // バブルボムを破壊    
    }
}
