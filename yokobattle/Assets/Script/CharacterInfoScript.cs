using UnityEngine;

public class CharacterInfoScript : MonoBehaviour
{
    [SerializeField] private string characterName = "Kobayashi";//キャラクターの名前
    [SerializeField] private int characterAge =21;//キャラクターの年齢
    [SerializeField] private string nickname = "P";//キャラクターのニックネーム
    [SerializeField] private string signatureLine = "At least I have chicken!";//キャラクターの決め台詞 

    public string CharacterName { get { return characterName; } }//名前のプロパティ
    public int CharacterAge { get { return characterAge; } }//年齢のプロパティ
    public string Nickname { get { return nickname; } }//ニックネームのプロパティ
    public string SignatureLine { get { return signatureLine; } }//決め台詞のプロパティ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
