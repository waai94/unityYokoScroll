using UnityEngine;

public class Attack_BubbleBomb : EnemyAttackObjectBase
{
    [SerializeField] private GameObject bubbleBombPrefab;//バブルボムのプレハブ
    [SerializeField] private int bombCount = 3;//生成するバブルボムの数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // StartAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void StartAttack() 
    {
        
        //バブルボム攻撃の具体的なロジックをここに実装
        enemyController.LookToTarget(); //敵が向いている方向を更新
        Debug.Log("Bubble Bomb Attack Started!");
        Vector2 direction = GetMyEnemyFacingDirection(); //敵の向いている方向を取得

        for (int i = 0; i < bombCount; i++)
        {
            GameObject bubbleBomb = Instantiate(bubbleBombPrefab, transform.position, Quaternion.identity);
            BulletController bombController = bubbleBomb.GetComponentInChildren<BulletController>();
            if (bombController == null)  bombController =bubbleBomb.GetComponent<BulletController>();
            if (!bombController)
            {
                Debug.LogWarning("BulletController component not found on bubbleBombPrefab.");
                continue;
            }

            direction.y += Random.Range(1f, 3f); //少し上下にばらつきを持たせる
            bombController.BulletInitialize(direction.normalized);
        }
        //例: 一定時間後に攻撃終了
        Invoke("AttackEnd", 2.0f); //2秒後に攻撃終了
    }

   
}
