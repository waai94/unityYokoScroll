using UnityEngine;

public class EnemyAttackObjectBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected EnemyAttackPatternBase parentPattern { get; set; }//親の攻撃パターンベース
    protected GameObject parentEnemyObject;//親の敵オブジェクト
    protected EnemyController enemyController;
    protected void Start()
    {
       
    }

    public void InitializeObject(GameObject parentEnemy,EnemyAttackPatternBase attackPatternBase)
    {
        parentPattern = attackPatternBase;
        parentEnemyObject = parentEnemy;
        
        //確認
        if (!enemyController)
        {
            Debug.LogError("EnemyAttackObjectBase: EnemyController is null!");
            return;
        }
        //確認
        if (!parentEnemyObject)
        {
            Debug.LogError("EnemyAttackObjectBase: Parent Enemy Object is null!");
            return;
        }
        //確認
        if (!parentPattern)
        {
            Debug.LogError("EnemyAttackObjectBase: Parent Pattern is null!");
            return;
        }
        TryGetEnemyControllerFromGameObject();//EnemyController取得
        StartAttack();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ////////////////////////////////////////////
    //
    /// </summary>
    // 攻撃開始
    protected void StartAttack()
    {
        //攻撃ロジックをここに実装
        //具体的な攻撃処理は派生クラスで実装
    }

    protected void AttackEnd()
    {
        parentPattern.EnemyAttackStart();//親の攻撃パターンを再度開始
        Destroy(this.gameObject);//オブジェクト破棄
    }

    // EnemyControllerを親の敵オブジェクトから取得を試みる
    protected void TryGetEnemyControllerFromGameObject()
    {
        if(!parentEnemyObject)
        {
            Debug.LogError("EnemyAttackObjectBase: Parent Enemy Object is null!");
            return;
        }
        EnemyController controller = parentEnemyObject.GetComponent<EnemyController>(); 
        if(!controller)
        {
            controller = parentEnemyObject.GetComponentInChildren<EnemyController>();

        }
        if(!controller)
        {
            Debug.LogError("EnemyAttackObjectBase: Could not find EnemyController in parent enemy object!");
            return;
        }
        enemyController = controller;
    }
}
