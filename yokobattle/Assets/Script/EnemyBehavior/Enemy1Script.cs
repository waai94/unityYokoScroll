using System;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyController enemyController;
    int behaviorPattern = 1; //パターンを記録する変数
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        BattleStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //バトル開始
    void BattleStart()
    {
        Debug.Log("Enemy1 Battle Start");
        Invoke("ChangeBattlePattern", 2.0f);
    }

    //次のパターンに変更
    void SetNextPattern(int next)
    {
        behaviorPattern = next;
        ChangeBattlePattern();
    }
    //パターン変更
    void ChangeBattlePattern()
    {
                switch (behaviorPattern)
        {
            case 1:
                BattlePattern1();
                break;
            default:
                Debug.Log("Enemy1 Battle Pattern Default");
                break;
        }
    }
    //パターン1: プレイヤーに向かってジャンプする
    void BattlePattern1()
            {
        Debug.Log("Enemy1 Battle Pattern 1");
        enemyController.JumpToPosition
            (
            transform.position,
            enemyController.Target.transform.position,
            2.0f
        );
        InvokeRepeating("ShootBulletTowardPlayer", 0.1f, 0.1f); //0.1秒ごとにプレイヤーに向かって弾を撃つ
        Invoke("EndBattlePattern1", 4.0f);
    }

    //パターン1終了
    void EndBattlePattern1()
    {
        CancelInvoke("ShootBulletTowardPlayer");
        SetNextPattern(1);
    }
    //プレイヤーに向かって弾を撃つ
    void ShootBulletTowardPlayer()
    {
        Debug.Log("Enemy1 Shoot Bullet Toward Player");
        Vector2 targetVector = enemyController.Target.transform.position;
        float angle = enemyController.angleToTarget(targetVector);
        enemyController.ShootBullet(null, this.transform.position, angle);

    }
}
