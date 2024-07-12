using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters")]
public class CharacterData : ScriptableObject
{
    //public string characterName;
    public int CharID;
    public Sprite charSprite;
    public int price;
    public bool isUnlocked;
    public bool isSelected;
    public GameObject characterPrefab;


    public void SetData(CharacterData charData)
    {
        this.CharID = charData.CharID;
        this.charSprite = charData.charSprite;
        this.price = charData.price;
        this.isUnlocked = charData.isUnlocked;
        this.isSelected = charData.isSelected;
    }

    public void SetLock(bool state)
    {
        this.isUnlocked = state;
    }

    public void SetSelect(bool state)
    {
        if(this.isUnlocked)
            this.isSelected = state;
    }
}
