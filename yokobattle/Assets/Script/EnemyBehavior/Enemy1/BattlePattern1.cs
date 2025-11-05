using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BattlePattern1 : EnemyBattlePatternBase
{
    public override void StartPattern()
    {
        Debug.Log("Enemy Pattern 1 Start");
        enemyController.JumpToPosition(transform.position, enemyController.Target.transform.position, 2f);
        InvokeRepeating(nameof(Shoot), 0.3f, 0.3f);
        Invoke(nameof(EndPattern), 3f);
    }

    public override void EndPattern()
    {
        CancelInvoke(nameof(Shoot));
        SendMessageUpwards("SetNextPattern", 1);
    }

    private void Shoot()
    {
        float angle = enemyController.angleToTarget(enemyController.Target.transform.position);
        enemyController.ShootBullet(null, transform.position, angle, 1f);
    }
}
