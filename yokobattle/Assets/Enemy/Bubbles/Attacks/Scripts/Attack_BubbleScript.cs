using UnityEngine;

public class Attack_BubbleScript : EnemyAttackObjectBase
{
    
    [SerializeField] private GameObject bubblePrefab;//バブルのプレハブ
    [SerializeField] private float bubbleFireInterval = 0.5f;//バブルを発射する間隔
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        Debug.Log("Bubble Fired!");
    }

    virtual protected void AttackEnd()
    {
        CancelInvoke("FireBubble"); //バブル発射の繰り返しを停止
        base.AttackEnd(); //親クラスのAttackEndを呼び出し
        Debug.Log("Bubble Attack Ended!");
    }

  
}

