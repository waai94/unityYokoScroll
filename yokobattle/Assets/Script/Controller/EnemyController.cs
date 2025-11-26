using UnityEngine;

/// <summary>
/// 敵の基本行動を制御するクラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private GameObject target;  //ターゲットとなるオブジェクト(例:プレイヤー)
    public GameObject Target { get { return target; } }
    private Rigidbody2D rb;
    [SerializeField] private GameObject defaultBulletPrehab;//デフォルトの弾のプレハブ
    protected float currentAngle;//現在の向きの角度
    protected SpriteRenderer spriteRenderer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void FixedUpdate()
    {
        //回転を適用
        UpdateSpriteDirection();
    }

    void UpdateSpriteDirection()//スプライトの向きを更新
    {
        if(spriteRenderer)
        {
            if(currentAngle > 90f || currentAngle < -90f)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    //ターゲットを見る
    public void LookToPosition(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;//ターゲットへの方向ベクトルを計算
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//角度を計算
       currentAngle = angle;
       

    }
    //ターゲットを見る
    public void LookToTarget()
    {
               if (!target)
        {
            Debug.LogWarning("Target not found for EnemyController.");
            return;
        }
        LookToPosition(target.transform.position);
    }
    public Vector2 GetEnemyFacingDirection()//敵の向いている方向を取得
    {
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
    }

    //放物線ジャンプを行うメソッド
    public void JumpToPosition(Vector2 startPos, Vector2 targetPos, float arcHeight)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float displacementX = targetPos.x - startPos.x;
        float displacementY = targetPos.y - startPos.y;

        // 放物線の頂点をゴールより高く設定
        float peakY = Mathf.Max(startPos.y, targetPos.y) + arcHeight;

        // 上昇・下降それぞれの高さを算出
        float heightUp = peakY - startPos.y;
        float heightDown = peakY - targetPos.y;

        // それぞれの時間を計算
        float timeUp = Mathf.Sqrt(2 * heightUp / gravity);
        float timeDown = Mathf.Sqrt(2 * heightDown / gravity);
        float totalTime = timeUp + timeDown;

        // 安全チェック
        if (float.IsNaN(totalTime) || totalTime <= 0f)
        {
            Debug.LogWarning("Invalid jump parameters.");
            return;
        }

        float velocityY = Mathf.Sqrt(2 * gravity * heightUp);
        float velocityX = displacementX / totalTime;

        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }

    //プレイヤーの位置に向かって放物線ジャンプ
    public void JumpToTarget(float arcHeight)//プレイヤーの位置に向かって放物線ジャンプ
    {
        if (!target)
        {
            Debug.LogWarning("Target not found for JumpToTarget.");
            return;
        }
        Vector2 playerPosition = target.transform.position;
        JumpToPosition(transform.position, playerPosition, arcHeight);
    }
    public float angleToTarget(Vector2 targetPosition)//ターゲットへの角度を取得
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;//ターゲットへの方向ベクトルを計算
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//角度を計算
        return angle;
    }

    //たまを発射する
    public void ShootBullet(GameObject bulletPrefab, Vector2 shootPosition, float angle, float distanceFromShooter)//弾のプレハブ、発射位置、発射角度、発射元からの距離
    {
        //弾のプレハブが指定されていない場合、デフォルトの弾のプレハブを使用
        if (!bulletPrefab)
        {
            
            if (!defaultBulletPrehab)
           {
                Debug.LogWarning("No bullet prefab assigned for shooting.");
                return;
           }
           else
           {
                bulletPrefab = defaultBulletPrehab;
            }
        }
        Vector2 spawnPoint = shootPosition + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * distanceFromShooter; //発射元からの距離を考慮した発射位置を計算
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint, Quaternion.Euler(0, 0, angle)); //弾を生成
        BulletController bulletController = bullet.GetComponentInChildren<BulletController>(); //弾のコントローラーを取得
        if (!bulletController)
        {
            Debug.LogWarning("BulletController component not found on bullet prefab.");
            return;
        }

        bulletController.BulletInitialize(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))); //弾の初期化
    }
}
