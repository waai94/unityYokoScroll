using UnityEngine;

public abstract class EnemyBattlePatternBase : MonoBehaviour
{
    protected EnemyController enemyController;

    public void Initialize(EnemyController controller)
    {
        enemyController = controller;
    }

    public abstract void StartPattern();
    public abstract void EndPattern();
}
