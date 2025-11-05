using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector2 direction = Vector2.right; // Default direction
    [SerializeField] private int defaultDamage = 1; // Default damage value
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        if(rb != null)
        {
            rb.linearVelocity = direction * speed; // Update velocity when direction changes
        }
    }

    // 弾を別スクリプトから初期化するためのメソッド
    public void BulletInitialize(Vector2 initDirection)
    {
        direction = initDirection.normalized;
       
        if(rb != null)
        {
            rb.linearVelocity = direction * speed; // Set velocity based on initialized direction
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(defaultDamage);
        }
        Destroy(gameObject);
    }
}
