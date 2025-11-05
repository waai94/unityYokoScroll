using UnityEngine;

public class Enemy1ExplostionBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;// Rigidbody2D component reference
    [SerializeField] private float speed = 5f; // Speed of the bullet
    [SerializeField] private GameObject miniBulletPrefab; // Prefab for mini bullets
    [SerializeField] int miniBulletCount = 8; // Number of mini bullets to spawn
    [SerializeField] private float miniBulletSpeed = 3f; // Speed of mini bullets
    [SerializeField] private float randomAngleRange = 15f; // Range for random angle variation
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnMiniBullets();
    }

    void SpawnMiniBullets()
    {
        for (int i = 0; i < miniBulletCount; i++)
        {
            // Calculate the angle for each mini bullet
            float angle = i * Mathf.PI * 2 / miniBulletCount + Random.RandomRange(-randomAngleRange * Mathf.Deg2Rad, randomAngleRange * Mathf.Deg2Rad);// Convert degrees to radians
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            // Instantiate the mini bullet
            GameObject miniBullet = Instantiate(miniBulletPrefab, transform.position, Quaternion.identity);
            // Set the velocity of the mini bullet
            BulletController miniBulletController = miniBullet.GetComponentInChildren<BulletController>();
            if (miniBulletController != null)
            {
                miniBulletController.BulletInitialize(direction);
            }
        }

    }
        
}
