using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BattlePattern2 : EnemyBattlePatternBase
{
    [SerializeField] private GameObject bounceBulletPrefab;
    private float timeToStop = 3.0f;
    private float timeToMove = 6.0f;

    public override void StartPattern()
    {
        Debug.Log("Enemy Pattern 2 Start");
        enemyController.JumpToPosition(transform.position, transform.position + new Vector3(0,5,0), 2f);
        // 弾を撃ち始める
        InvokeRepeating(nameof(ShootBounceBullet), 0.3f, 0.3f);

        // 一定時間後に弾を停止
        Invoke(nameof(StopAllBullets), timeToStop);

        // 停止後、さらに時間をおいて再始動
        Invoke(nameof(MoveAllBounceBullet), timeToMove);

        // 全体の終了処理
        Invoke(nameof(EndPattern), timeToMove + 3.0f);
    }

    public override void EndPattern()
    {
        CancelInvoke(nameof(ShootBounceBullet));
        CancelInvoke(nameof(StopAllBullets));
        CancelInvoke(nameof(MoveAllBounceBullet));

        Debug.Log("Enemy Pattern 2 End");

        // 次のパターンへ（Enemy1Scriptに通知）
        SendMessageUpwards("SetNextPattern", 0);
    }

    private void ShootBounceBullet()
    {
        if (enemyController == null || enemyController.Target == null) return;

        Debug.Log("Enemy Pattern 2: Shoot Bounce Bullet");
        Vector2 targetVector = enemyController.Target.transform.position;
        for (int i = 0; i<10; i++)
        {
            float angleOffset = i * 36f; // 360度を10分割
            float angle = enemyController.angleToTarget(targetVector) + angleOffset;
            enemyController.ShootBullet(bounceBulletPrefab, transform.position, angle, 1f);
        }
    }

    private void StopAllBullets()
    {
        Debug.Log("Enemy Pattern 2: Stop All Bullets");
        CancelInvoke(nameof(ShootBounceBullet));

        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bulletObjects)
        {
            var bounceScript = bullet.GetComponentInChildren<Enemy1BounceBulletScript>();
            if (bounceScript != null)
            {
                bounceScript.StopMovement();
            }
        }
    }

    private void MoveAllBounceBullet()
    {
        Debug.Log("Enemy Pattern 2: Move All Bounce Bullets");
        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bulletObjects)
        {
            var bounceScript = bullet.GetComponentInChildren<Enemy1BounceBulletScript>();
            if (bounceScript != null)
            {
                bounceScript.MoveToTarget();
            }
        }
    }
}
