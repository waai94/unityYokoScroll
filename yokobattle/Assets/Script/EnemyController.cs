using UnityEngine;

/// <summary>
/// 敵の基本行動を制御するクラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private GameObject target;  //ターゲットとなるオブジェクト(例:プレイヤー)
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ターゲットを見る
    public void LookToPosition(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;//ターゲットへの方向ベクトルを計算
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//角度を計算
        rb.rotation = angle;
        
    }

    public void JumpToPosition(Vector2 startPos, Vector2 targetPos, float arcHeight)//放物線を描いてジャンプする
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);//重力加速度を取得

        float displacementX = targetPos.x - startPos.x;//水平距離
        float displacementY = targetPos.y - startPos.y;//垂直距離

        float time = Mathf.Sqrt(2 * arcHeight / gravity) +
                     Mathf.Sqrt(2 * (displacementY - arcHeight) / gravity);//飛行時間を計算

        float velocityY = Mathf.Sqrt(2 * gravity * arcHeight);//垂直速度を計算
        float velocityX = displacementX / time;//   水平速度を計算

        rb.linearVelocity = new Vector2(velocityX, velocityY);//速度を設定
    }

    public float angleToTarget(Vector2 targetPosition)//ターゲットへの角度を取得
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;//ターゲットへの方向ベクトルを計算
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//角度を計算
        return angle;
    }
}
