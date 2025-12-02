using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Attack_AttractBubbles : EnemyAttackObjectBase
{
    [SerializeField] private float attractDuration = 3.0f; //バブルを引き寄せる時間
    [SerializeField] private float attractForce = 5.0f; //引き寄せる力の強さ
    [SerializeField] private float killBubbleRadius = 1.0f; //バブルを消す範囲の半径

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
       List<GameObject> bubbles = FindBubbles();
         AttractBubbles(bubbles);
        Invoke("AttackEnd", attractDuration); //2秒後に攻撃終了
    }

    

    virtual protected void AttackEnd()
    {
        CancelInvoke("FireBubble"); //バブル発射の繰り返しを停止
        base.AttackEnd(); //親クラスのAttackEndを呼び出し
        Debug.Log("Bubble Attack Ended!");
    }

    // バブルを探すメソッド
    List<GameObject> FindBubbles()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> bubbles = new List<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("bubble"))
            {
                bubbles.Add(obj);
            }
        }
        Debug.Log($"Found {bubbles.Count} bubbles.");
        return bubbles;
    }

    void AttractBubbles(List<GameObject> bubbles)
    {
        foreach (GameObject bubble in bubbles)
        {
            if (bubble != null)
            {
                GameObject bubbleObject = bubble.GetComponentInChildren<BulletController>(true)?.gameObject;
                if(!bubbleObject)
                {
                    Debug.LogWarning("Bubble does not have a BulletController component.");
                    continue;
                }
                Vector3 direction = (parentEnemyObject.transform.position - bubbleObject.transform.position).normalized;
                Vector2 direction2D = new Vector2(direction.x, direction.y);
                bubble.GetComponentInChildren<BulletController>()?.BulletInitialize(direction2D);
                bubble.GetComponentInChildren<Rigidbody2D>()?.AddForce(direction2D * attractForce, ForceMode2D.Impulse);
                // Debug log to confirm attraction

                if (!bubble.GetComponentInChildren<Rigidbody2D>())
                {
                    Debug.LogWarning("Bubble does not have a Rigidbody2D component.");
                }
                else
                {
                    Debug.Log("Attracting bubble towards enemy.");
                }
            }
        }
    }
}

