using UnityEngine;

public class Attack_BubbleScript : EnemyAttackObjectBase
{
    
    [SerializeField] private GameObject bubblePrefab;//バブルのプレハブ
    [SerializeField] private float bubbleFireInterval = 0.5f;//バブルを発射する間隔
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual protected void StartAttack()
    {
        //バブル攻撃の具体的なロジックをここに実装
        Debug.Log("Bubble Attack Started!");
        //例: 一定間隔でバブルを発射
        InvokeRepeating("FireBubble", 0.0f, bubbleFireInterval);
        //例: 一定時間後に攻撃終了
        Invoke("AttackEnd", 2.0f); //2秒後に攻撃終了
    }

    void FireBubble()
    {
        //バブルを発射するロジックをここに実装
        GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        BubbleControllerScript bubbleScript = bubbleInstance.GetComponentInChildren<BubbleControllerScript>();
        Vector2 shootDirection = GetMyEnemyFacingDirection();//敵の向いている方向を取得
        //shootDirection.Normalize(); //方向ベクトルを正規化
        //shootDirection.y = Random.Range(-0.5f, 0.5f); //少し上下にばらつきを持たせる
        shootDirection = new Vector2(1.0f,0.0f); //水平に発射するように固定
        if (!bubbleScript)
        {
            Debug.LogWarning("BubbleControllerScript component not found on bubblePrefab.");
            return;
        }
        else
        {
            Debug.Log("bubbleScript is Valid");
        }
            bubbleScript.BulletInitialize(shootDirection); //敵の向いている方向に発射
    }

    virtual protected void AttackEnd()
    {
        CancelInvoke("FireBubble"); //バブル発射の繰り返しを停止
        base.AttackEnd(); //親クラスのAttackEndを呼び出し
        Debug.Log("Bubble Attack Ended!");
    }

  
}

