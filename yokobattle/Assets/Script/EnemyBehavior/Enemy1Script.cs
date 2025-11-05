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

    void BattleStart()
    {
        Debug.Log("Enemy1 Battle Start");
        Invoke("ChangeBattlePattern", 2.0f);
    }

    void SetNextPattern(int next)
    {
        behaviorPattern = next;
        ChangeBattlePattern();
    }
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

    }
}
