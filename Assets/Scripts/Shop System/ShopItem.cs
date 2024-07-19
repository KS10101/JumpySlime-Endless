using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image IconSprite;
    public TextMeshProUGUI NumberOfCoinsText;
    public TextMeshProUGUI Price;
    public Button BuyButton;
    private int numberOfCoins;

    public void Init(StoreItemData itemData)
    {
        this.IconSprite.sprite = itemData.IconSprite;
        this.NumberOfCoinsText.text = "x" + itemData.NumberOfCoins.ToString();
        this.Price.text = itemData.price.ToString();
        this.numberOfCoins = itemData.NumberOfCoins;
    }

    private void OnEnable()
    {
        BuyButton.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        BuyButton.onClick.RemoveListener(BuyItem);
    }

    private void BuyItem()
    {
        Debug.Log("BUY");
        ScoreManager.instance.AddnSaveCoinsData(this.numberOfCoins);
        StorePanel.instance.UpdateCoinText(ScoreManager.instance.GetCoinsData());
    }
}
