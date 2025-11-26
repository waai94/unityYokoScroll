using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 対象のキャラクターの名前をUIに表示するスクリプト
/// 
/// </summary>
public class NameUIManager : MonoBehaviour
{
    enum whichName
    {
        Player,
        Enemy
    }

    [SerializeField] private whichName nameType = whichName.Player;//名前の種類を選択する列挙型

    TextMeshPro TextMeshPro;//TextMeshProコンポーネントへの参照



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI TextMeshPro= GetComponent<TextMeshProUGUI>(); //TextMeshProコンポーネントを取得

        if (!TextMeshPro)
        {
            Debug.LogError("TextMeshPro component not found on " + gameObject.name);
            return;
        }

        string characterName = GetName();
        TextMeshPro.text = characterName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GetName()
    {
        string name = "";
        string findTag = "";
        switch(nameType)
        {
            case whichName.Player:
                findTag = "Player";
                break;
            case whichName.Enemy:
                findTag = "Enemy";
                break;
        }

        GameObject character = GameObject.FindGameObjectWithTag(findTag);
        Debug.Log($"Finding character with tag: {findTag} {character}");
        if (!character)return $"{findTag} not found";
        CharacterInfoScript info = character.GetComponent<CharacterInfoScript>();

        if(!info)
        {
            info = character.GetComponentInChildren<CharacterInfoScript>(); //子オブジェクトにある場合
        }
        if(!info)return $"{findTag} :: CharacterInfoScript not found";
        name = info.CharacterName;
        return name;
    }
}
