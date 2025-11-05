using UnityEngine;

public class Enemy1BounceBulletScript : MonoBehaviour
{ 
    private float speed = 5.0f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // 弾の移動を停止させるメソッド(Enemy1が呼び出す)
    public void StopMovement()
    {
              
        if (rb != null)
        {
            speed =rb.linearVelocity.magnitude;
            rb.linearVelocity = Vector2.zero;
        }
    }
    // 弾をターゲットに向かって移動させるメソッド
    public void MoveToTarget()
    {
        // 弾の移動処理
       Debug.Log("Bounce Bullet MoveToTarget called");
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;


            rb.linearVelocity = direction * 8;
        }
    }

}
