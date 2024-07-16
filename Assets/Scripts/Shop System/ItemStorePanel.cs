using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStorePanel : MonoBehaviour
{
    public static ItemStorePanel instance;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI coinsAmountText;

    [SerializeField] private Button storeCloseButton;
    [SerializeField] private Button ScrollLeftBut;
    [SerializeField] private Button ScrollRightBut;

    [SerializeField] private GameObject storePanel;
    [SerializeField] private AudioClip clickSFX;

    [SerializeField] private float scrollSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        storeCloseButton.onClick.AddListener(CloseStore);
        ScrollLeftBut.onClick.AddListener(ScrollLeft);
        ScrollRightBut.onClick.AddListener(ScrollRight);
    }

    private void OnDisable()
    {
        storeCloseButton.onClick.RemoveListener(CloseStore);
        ScrollLeftBut.onClick.RemoveListener(ScrollLeft);
        ScrollRightBut.onClick.RemoveListener(ScrollRight);
    }

    private void ScrollLeft()
    {
        if(scrollRect.horizontalNormalizedPosition > 0)
            scrollRect.horizontalNormalizedPosition -= scrollSpeed;
    }

    public void ScrollRight()
    {
        if (scrollRect.horizontalNormalizedPosition < 1)
            scrollRect.horizontalNormalizedPosition += scrollSpeed;
    }

    public void UpdateCoinText(int coins)
    {
        coinsAmountText.text = coins.ToString();
    }

    private void CloseStore()
    {

        CharacterManager.instance.SetCharacterState();
        CharacterManager.instance.UpdateScriptableListDataState();
        storePanel.SetActive(false);
        AudioManager.instance.PlaySFX(clickSFX);
    }
}
