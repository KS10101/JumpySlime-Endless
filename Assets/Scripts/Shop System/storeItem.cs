using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    //public string charName;
    public int charID;
    public Image charSprite;
    public int price;
    public TextMeshProUGUI priceText;
    public bool isUnlocked;
    public bool isSelected;
    public Image lockImg;
    public Image unlockImg;
    public Button buyButton;
    public Button selectButton;
    public Button selectedButton;

    private void OnEnable()
    {
        buyButton.onClick.AddListener(BuyItem);
        selectButton.onClick.AddListener(SelectItem);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(BuyItem);
        selectButton.onClick.RemoveListener(SelectItem);
    }

    public void Init(CharacterData charData)
    {
        charID = charData.CharID;
        charSprite.sprite = charData.charSprite;
        price = charData.price;
        priceText.text = charData.price.ToString();
        isUnlocked = charData.isUnlocked;
        isSelected = charData.isSelected;
        priceText.gameObject.SetActive(!isUnlocked);
        unlockImg.enabled = isUnlocked;
        lockImg.enabled = !isUnlocked;
        buyButton.gameObject.SetActive(!isUnlocked);
        selectButton.gameObject.SetActive(isUnlocked);
        selectedButton.gameObject.SetActive(isSelected);
    }

    private CharacterData CreateCharacterData()
    {
        CharacterData data = new CharacterData
        {
            CharID = this.charID,
            charSprite = this.charSprite.sprite,
            price = this.price,
            isUnlocked = this.isUnlocked,
            isSelected = this.isSelected

        };

        return data;
    }

    private void BuyItem()
    {
        int coins = ScoreManager.instance.GetCoinsData();
        if (coins >= price && !isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            priceText.gameObject.SetActive(false);
            lockImg.enabled = false;
            unlockImg.enabled = true;
            isUnlocked = true;
            CharacterManager.instance.BuyStoreitem(this.charID);
            ScoreManager.instance.SaveCoinsData(coins - price);
            ItemStorePanel.instance.UpdateCoinText(ScoreManager.instance.GetCoinsData());
        }
        AudioManager.instance.PlaySFX();
    }

    private void SelectItem()
    {
        if (isUnlocked && !isSelected)
        {
            selectButton.gameObject.SetActive(false);
            selectedButton.gameObject.SetActive(true);
            isSelected = true;
            CharacterManager.instance.SelectStoreItem(this.charID);
            PlayerController.instance.SwitchCharacter();
        }
        AudioManager.instance.PlaySFX();
    }
}
