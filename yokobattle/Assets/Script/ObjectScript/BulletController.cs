using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected Vector2 direction = Vector2.right; // Default direction
    [SerializeField] protected float defaultDamage = 1; // Default damage value
    [SerializeField] protected float lifetime = 5f; // Lifetime of the bullet in seconds
    public float LifeTime { get { return lifetime; } set { lifetime = value; } }// Public property to get/set lifetime
    [SerializeField] protected float gravityScale = 0f; // Gravity scale for the bullet
    protected Rigidbody2D rb;
    void Awake()
    {
      
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(lifetime > 0f)
        {
            Invoke(nameof(DestroyBullet), lifetime); // Schedule destruction after lifetime
        }
        if(!rb)
        {
            Debug.LogWarning("Rigidbody2D component not found on bullet.");
            return;
        }
        rb.gravityScale = gravityScale;// Set gravity scale
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
        rb = GetComponent<Rigidbody2D>();
        direction = initDirection.normalized;
    
        if (rb != null)
        {
            rb.linearVelocity = direction * speed; // Set velocity based on initialized direction
            rb.linearDamping = 0f; // No linear damping by default
            
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on bullet.");
        }
      
    }
    // Update is called once per frame
    protected void Update()
    {
        
    }

    // 衝突時の処理
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet collided with: " + other.gameObject.name);
        HealthManager health = other.gameObject.GetComponent<HealthManager>(); // Check for HealthManager component
        if (!health) health = other.gameObject.GetComponentInParent<HealthManager>();// 親オブジェクトも確認
        if (!health) health = other.gameObject.GetComponentInChildren<HealthManager>();//
  
        if (health != null)
        {
            health.TakeDamage(defaultDamage);
        }else
        {
            Debug.Log("Collided object has no HealthManager.");
        }
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager health = collision.gameObject.GetComponent<HealthManager>(); // Check for HealthManager component
        if (!health) health = collision.gameObject.GetComponentInParent<HealthManager>();// 親オブジェクトも確認
        if (!health) health = collision.gameObject.GetComponentInChildren<HealthManager>();//
        if (health != null)
        {
            health.TakeDamage(defaultDamage);
        }
        else
        {
            Debug.Log("Collided object has no HealthManager.");
        }
        Destroy(gameObject);
    }

    // 弾を消去するメソッド
    protected void DestroyBullet()
    {
               Destroy(gameObject);
    }
}
