using System;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    private EnemyController enemyController;
    private int behaviorPattern = 1; // 現在のパターン番号
    [SerializeField] private GameObject bounceBulletPrefab;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        BattleStart();
    }

    // ===== バトル開始 =====
    void BattleStart()
    {
        Debug.Log("Enemy1 Battle Start");
        Invoke(nameof(ChangeBattlePattern), 2.0f);
    }

    // ===== パターン変更 =====
    void ChangeBattlePattern()
    {
        switch (behaviorPattern)
        {
            case 1:
                BattlePattern1();
                break;
            case 2:
                BattlePattern2();
                break;
            case 3:
                BattlePattern3();
                break;
            default:
                Debug.Log("Enemy1 Battle Pattern Default");
                break;
        }
    }

    // ===== 共通の終了処理 =====
    void EndCurrentPattern(int nextPattern)
    {
        CancelInvoke(); // 全Invoke停止
        behaviorPattern = nextPattern;
        ChangeBattlePattern();
    }

    // ===== パターン1：プレイヤーに向かってジャンプしながら攻撃 =====
    void BattlePattern1()
    {
        Debug.Log("Enemy1 Battle Pattern 1");
        enemyController.JumpToPosition(transform.position, enemyController.Target.transform.position, 2.0f);
        InvokeRepeating(nameof(ShootBulletTowardPlayer), 0.3f, 0.3f);
        Invoke(nameof(EndBattlePattern1), 3.0f);
    }

    void EndBattlePattern1()
    {
        EndCurrentPattern(2);
    }

    // ===== パターン2：バウンス弾を発射・停止・再発射 =====
    void BattlePattern2()
    {
        Debug.Log("Enemy1 Battle Pattern 2");

        float timeToStop = 3.0f;
        float timeToMove = timeToStop + 3.0f;

        InvokeRepeating(nameof(ShootBounceBullet), 0.3f, 0.3f);
        Invoke(nameof(StopAllBullets), timeToStop);
        Invoke(nameof(MoveAllBounceBullet), timeToMove);
        Invoke(nameof(EndBattlePattern2), timeToMove + 3.0f);
    }

    void EndBattlePattern2()
    {
        EndCurrentPattern(3);
    }

    // ===== パターン3：Hello World!を出力して次に戻る =====
    void BattlePattern3()
    {
        //パターンを考案中・・・
        // ここに新しい攻撃パターンを実装する
        Debug.Log("Enemy1 Battle Pattern 3");
        Debug.Log("Hello World!");
        Invoke(nameof(EndBattlePattern3), 2.0f); // 2秒後に戻る
    }

    void EndBattlePattern3()
    {
        EndCurrentPattern(1);
    }

    // ===== 弾の挙動関連 =====

    // プレイヤーに向かって弾を撃つ
    void ShootBulletTowardPlayer()
    {
        Debug.Log("Enemy1 Shoot Bullet Toward Player");
        Vector2 targetVector = enemyController.Target.transform.position;
        float angle = enemyController.angleToTarget(targetVector);
        enemyController.ShootBullet(null, transform.position, angle, 1f);
    }

    // バウンス弾を撃つ
    void ShootBounceBullet()
    {
        Debug.Log("Enemy1 Shoot Bounce Bullet");
        Vector2 targetVector = enemyController.Target.transform.position;
        for (int i = 0; i < 10; i++)
        {
            float angleOffset = i * 36f; // 360度を10分割
            float angle = enemyController.angleToTarget(targetVector) + angleOffset;
            enemyController.ShootBullet(bounceBulletPrefab, transform.position, angle, 1f);
        }
    }

    // すべてのバウンス弾を停止
    void StopAllBullets()
    {
        CancelInvoke(nameof(ShootBounceBullet));
        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bulletObjects)
        {
            if (bullet.GetComponentInChildren<Enemy1BounceBulletScript>() != null)
            {
                bullet.GetComponent<Enemy1BounceBulletScript>().StopMovement();
            }
        }
    }

    // すべてのバウンス弾をターゲットに向かって移動
    void MoveAllBounceBullet()
    {
        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bulletObjects)
        {
            if (bullet.GetComponentInChildren<Enemy1BounceBulletScript>() != null)
            {
                bullet.GetComponent<Enemy1BounceBulletScript>().MoveToTarget();
            }
        }
    }
}
