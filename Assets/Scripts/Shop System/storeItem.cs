using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    //public string charName;
    public Image charSprite;
    public TextMeshProUGUI price;
    public bool isUnlocked;
    public Image lockImg;
    public Image unlockImg;
    public GameObject selectButton;
    public GameObject buyButton;

    public void Init(CharacterData charData)
    {
        charSprite.sprite = charData.charSprite;
        price.text = charData.price.ToString();
        isUnlocked = charData.isUnlocked;
        price.gameObject.SetActive(!isUnlocked);
        unlockImg.enabled = isUnlocked;
        lockImg.enabled = !isUnlocked;
        selectButton.SetActive(isUnlocked);
        buyButton.SetActive(!isUnlocked);
    }
}
