using UnityEngine;
using System.Collections.Generic;
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


    protected class AttackPatternState
    {
        public string name; //攻撃パターン名 attack pattern name


        public GameObject prefab; //攻撃パターンのプレハブ
        public float currentWeight = 0; //この攻撃パターンの現在の重み
        public determingAttackType attackType = determingAttackType.none; //攻撃タイプ
        public float defaultWeight = 0; //デフォルトの重み
        public float addedWeight = 1; //このクラス以外が発動したときに追加される重み
        public float weightingFactorByDetermingAttackType = 1; //重み付け係数
        //!!重みの計算結果が同時だったら、リストの先頭に近い方が優先される!!//
    }


    [SerializeField] protected List<AttackPatternState> attackPatterns; //攻撃パターンのリスト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        //攻撃ロジックをここに実装
        AttackPatternState attackPattern = DetermineAttackPattern();
        if (attackPattern != null)
        {
            //選択された攻撃パターンに基づいて攻撃を実行
            Instantiate(attackPattern.prefab, transform.position, transform.rotation);
        }
    }

    //攻撃パターンを決定するメソッド
    AttackPatternState DetermineAttackPattern()
    {
        //攻撃パターンを決定するロジックをここに実装
        AttackPatternState selectedPattern = null;
        foreach (var pattern in attackPatterns)
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
                    float healthPercentage = 0.5f; //仮の体力割合 実際はHealthManagerから取得
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
}