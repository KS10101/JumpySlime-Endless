using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public List<CharacterData> CharList;
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform storeItemContainer;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < CharList.Count; i++)
        {
            CharList[i].CharID = i;
        }

    }

    public void InitiateStoreItems()
    {
        DeleteGeneratedItems();

        for(int i=0 ; i<CharList.Count ; i++)
        {
            GameObject item = Instantiate(storeItemPrefab, storeItemContainer);
            item.GetComponent<StoreItem>().Init(CharList[i]);
        }
    }

    public void DeleteGeneratedItems()
    {
        int ItemNum = storeItemContainer.childCount;

        if (ItemNum == 0) return;

        for (int i = 0; i < ItemNum; i++)
        {
            Destroy(storeItemContainer.GetChild(i).gameObject);
        }
    }

    public void RefreshItems()
    {
        DeleteGeneratedItems();
        InitiateStoreItems();
    }

    public void UpdateCharacterData(CharacterData charData)
    {
        for (int i = 0; i < CharList.Count; i++)
        {
            if (CharList[i].CharID == charData.CharID)
                CharList[i].SetData(charData);
        }
    }

    public void BuyStoreitem(int charID)
    {
        for (int i = 0; i < CharList.Count; i++)
        {
            if (CharList[i].CharID == charID)
                CharList[i].SetLock(true);

        }
        RefreshItems();
    }

    public void SelectStoreItem(int charID)
    {
        for (int i = 0; i < CharList.Count; i++)
        {
            if (CharList[i].CharID == charID)
                CharList[i].SetSelect(true);
            else
                CharList[i].SetSelect(false);
        }
        RefreshItems();
    }

    public GameObject GetSelectedCharacter()
    {
        GameObject CharPrefab = new GameObject();
        for (int i = 0; i < CharList.Count; i++)
        {
            if (CharList[i].isSelected)
            {
                CharPrefab = CharList[i].characterPrefab.gameObject;
                break;
            }
        }

        return CharPrefab;
    }
}
