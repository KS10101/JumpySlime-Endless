using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterManager : MonoBehaviour
{
    public static characterManager instance;
    public List<CharacterData> CharList;
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform storeItemContainer;
    [SerializeField] private Button storeCloseButton;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private AudioClip clickSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        storeCloseButton.onClick.AddListener(CloseStore);
    }

    private void OnDisable()
    {
        storeCloseButton.onClick.RemoveListener(CloseStore);
    }


    public void InitiateStoreItems()
    {
        for(int i=0 ; i<CharList.Count ; i++)
        {
            GameObject item = Instantiate(storeItemPrefab, storeItemContainer);
            item.GetComponent<storeItem>().Init(CharList[i]);
        }
    }

    private void CloseStore()
    {
        storePanel.SetActive(false);
        AudioManager.instance.PlaySFX(clickSFX);
    }




}
