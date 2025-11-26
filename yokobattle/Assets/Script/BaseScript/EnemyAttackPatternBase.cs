using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// 
/// </summary>
public class EnemyAttackPatternBase : MonoBehaviour
{

    //攻撃を決める重みにつかう要素の列挙型
    public enum determingAttackType
    {
        distanceBased,//距離ベース
        healthBased,//体力ベース
        timeBased,//時間ベース
        none//なし

    }

    public enum condtionExpressionType
    {
        greaterThan,//より大きい
        lessThan,//より小さい
        equalTo,//等しい
        none//なし
    }
    [Serializable]protected class AttackPatternState
    {
        public string name; //攻撃パターン名 attack pattern name


        public GameObject prefab; //攻撃パターンのプレハブ
        public float currentWeight = 0; //この攻撃パターンの現在の重み
        public determingAttackType attackType = determingAttackType.none; //攻撃タイプ
        public determingAttackType attackCondtion = determingAttackType.none; //攻撃条件タイプ
        public condtionExpressionType condtionExpression = condtionExpressionType.none; //条件の表現タイプ

        public float attackConditionValue = 0; //攻撃条件の値
        public float defaultWeight = 0; //デフォルトの重み
        public float addedWeight = 1; //このクラス以外が発動したときに追加される重み
        public float weightingFactorByDetermingAttackType = 1; //重み付け係数

        //!!重みの計算結果が同時だったら、リストの先頭に近い方が優先される!!//
    }


    [SerializeField] protected List<AttackPatternState> attackPatterns; //攻撃パターンのリスト
    [SerializeField] protected float delayForFirstAttack = 1.0f; //最初の攻撃までの遅延時間
    HealthManager healthManager; //敵の体力管理コンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("EnemyAttackStart", delayForFirstAttack);//最初の攻撃を遅延させて開始
        healthManager = GetComponent<HealthManager>();
        if(healthManager == null)
        {
            healthManager = GetComponentInChildren<HealthManager>(); //子オブジェクトからも探す
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //攻撃を実行するメソッド
  public  void EnemyAttackStart()
    {
        //攻撃ロジックをここに実装
        AttackPatternState attackPattern = DetermineAttackPattern();
        if (attackPattern != null)
        {
            //選択された攻撃パターンに基づいて攻撃を実行
         GameObject attackObject = Instantiate(attackPattern.prefab, transform.position, transform.rotation);
            EnemyAttackObjectBase attackScript = attackObject.GetComponent<EnemyAttackObjectBase>();
            Debug.Assert(attackScript != null, "EnemyAttackObjectBase component not found on attack prefab.");
            Debug.Log("Executing Attack Pattern: " + attackPattern.name);
            attackScript.InitializeObject(this.gameObject, this);
            //必要に応じてattackScriptのプロパティを設定

        }
    }

    //攻撃パターンを決定するメソッド
    AttackPatternState DetermineAttackPattern()
    {
        //攻撃パターンを決定するロジックをここに実装
        AttackPatternState selectedPattern = null;
       List<AttackPatternState> validPatterns = new List<AttackPatternState>();
        validPatterns = FilterAttackPetterns();
        foreach (var pattern in validPatterns)
        {
            //重みの計算ロジックをここに実装
            float weight = pattern.currentWeight;
            //例: 距離ベースの重み付け
            switch(pattern.attackType)
            {
                case determingAttackType.distanceBased:
                    //距離に基づく重み付けの計算例
                    float distanceToPlayer = Vector3.Distance(transform.position, /*プレイヤーの位置*/ Vector3.zero);
                    weight += (1.0f / (distanceToPlayer + 1.0f)) * pattern.weightingFactorByDetermingAttackType;
                    break;
                case determingAttackType.healthBased:
                    //体力に基づく重み付けの計算例
                    float healthPercentage = healthManager.GetHealthPercentage(); //仮の体力割合 実際はHealthManagerから取得
                    weight += (1.0f - healthPercentage) * pattern.weightingFactorByDetermingAttackType;
                    break;
                case determingAttackType.timeBased:
                 //考え中・・・
                    break;
                case determingAttackType.none:
                default:
                    //特に重み付けを行わない
                    break;
            }
            
            //最も高い重みの攻撃パターンを選択
            if (selectedPattern == null || weight > selectedPattern.currentWeight) //重みが高い場合に更新
            {
                selectedPattern = pattern;//選択された攻撃パターンを更新
            }
        }
        AddWeightForNotSelectedPatterns(selectedPattern);

        return selectedPattern;
    }

    //選択されなかった攻撃パターンに重みを追加するメソッドまた、選択された攻撃パターンの重みをリセットするメソッド
    void AddWeightForNotSelectedPatterns(AttackPatternState selectedPattern)
    {
        //選択されなかった攻撃パターンに重みを追加するロジックをここに実装
        foreach (var pattern in attackPatterns)
        {
            if (pattern != selectedPattern)
            {
                pattern.currentWeight += pattern.addedWeight;//重みを追加
            }else
            {
             　　pattern.currentWeight = pattern.defaultWeight;//選択されたパターンの重みをデフォルトにリセット
            }
        }
    }
    //攻撃パターンのリストを取得するメソッド
    List<AttackPatternState> FilterAttackPetterns()
    {
        List<AttackPatternState> result = new List<AttackPatternState>();

        foreach (var pattern in attackPatterns)
        {
            bool shouldPass = EvaluatePattern(pattern);
            if (shouldPass)
            {
                result.Add(pattern);
                Debug.Log("Pattern " + pattern.name + " passed the evaluation.");
            }
        }

        return result;
    }
    //攻撃パターンの条件を評価するメソッド
    bool EvaluatePattern(AttackPatternState pattern)
    {
        switch (pattern.attackCondtion)
        {
            case determingAttackType.distanceBased:
                float distance = Vector3.Distance(transform.position, Vector3.zero);
                return EvaluateCondition(distance, pattern);

            case determingAttackType.healthBased:
                float hpPercent = healthManager.GetHealthPercentage();
                Debug.Log("Evaluating health-based condition: HP% = " + hpPercent +"Result is " + EvaluateCondition(hpPercent, pattern));
                return EvaluateCondition(hpPercent, pattern);

            case determingAttackType.timeBased:
                //考え中の条件ができたらここへ
                return true;

            case determingAttackType.none:
            default:
                return true;
        }
    }
    //条件を評価するメソッド
    bool EvaluateCondition(float targetValue, AttackPatternState pattern)
    {
        switch (pattern.condtionExpression)
        {
            case condtionExpressionType.greaterThan://より大きいA
                return targetValue > pattern.attackConditionValue;

            case condtionExpressionType.lessThan://より小さい
                Debug.Log("Evaluating lessThan condition: Target Value = " + targetValue + ", Condition Value = " + pattern.attackConditionValue);
                return targetValue < pattern.attackConditionValue;

            case condtionExpressionType.equalTo://ほぼ等しい
                return Mathf.Abs(targetValue - pattern.attackConditionValue) < 0.01f;

            case condtionExpressionType.none:
            default:
                return true;
        }
    }
}