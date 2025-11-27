using UnityEngine;

public class Attack_FireRandomBubble : EnemyAttackObjectBase
{/// <summary>
 /// バブルをランダムな方向に発射する攻撃パターン
 ///    

    [SerializeField] private GameObject bubblePrefab;//バブルのプレハブ
    [SerializeField] private float bubbleFireInterval = 0.5f;//バブルを発射する間隔
    [SerializeField] private float addedAngleRangePerShoot = 5;//発射ごとに追加される角度の範囲
    private float angle = 0;
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
    //
    void FireBubble()
    {
     
      
      
        for (int i = 0; i < 8; i++) //1回ループ（将来の拡張のためにループを使用）
        {
            //バブルを発射するロジックをここに実装
            GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            BubbleControllerScript bubbleScript = bubbleInstance.GetComponentInChildren<BubbleControllerScript>();

            float shootAngle = angle + i * 45f; //8方向に均等に分散させる
            Vector2 shootDirection = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad));//角度から方向ベクトルを計算
            shootDirection.Normalize(); //方向ベクトルを正規化
            

            if (!bubbleScript)
            {
                Debug.LogWarning("BubbleControllerScript component not found on bubblePrefab.");
                return;
            }

            bubbleScript.LifeTime = 10.0f;//次の攻撃に使うので寿命を延ばす
            bubbleScript.BulletInitialize(shootDirection); //敵の向いている方向に発射
           // Debug.Log("Bubble fired in direction: " + shootDirection);
        }
        angle += Random.Range(0.1f, addedAngleRangePerShoot); //次の発射時に角度をランダムに変更

    }

    virtual protected void AttackEnd()
    {
        CancelInvoke("FireBubble"); //バブル発射の繰り返しを停止
        base.AttackEnd(); //親クラスのAttackEndを呼び出し
        Debug.Log("Bubble Attack Ended!");
    }

  
}

