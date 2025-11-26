using UnityEngine;

public class JumpToPlayer : EnemyAttackObjectBase
{
    [SerializeField] private float jumpForce = 8f; // ジャンプ力
    [SerializeField] private float jumpCooldown = 2f; // ジャンプのクールダウン時間
    private bool canJump = true; // ジャンプ可能かどうか

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
        enemyController.JumpToTarget(jumpForce);
        Invoke("AttackEnd", jumpCooldown); //2秒後に攻撃終了
    }

    

    virtual protected void AttackEnd()
    {
        CancelInvoke("FireBubble"); //バブル発射の繰り返しを停止
        base.AttackEnd(); //親クラスのAttackEndを呼び出し
        Debug.Log("Bubble Attack Ended!");
    }

  
}

