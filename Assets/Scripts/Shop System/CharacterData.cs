using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters")]
public class CharacterData : ScriptableObject
{
    //public string characterName;
    public Sprite charSprite;
    public int price;
    public bool isUnlocked;
    public Image lockImg;
    //public GameObject characterPrefab;

}
