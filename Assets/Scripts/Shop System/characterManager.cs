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

    public void ActivateCharacter(Transform parentTransform)
    {
        if(parentTransform.childCount >= 1)
        {
            DeactivateChar(parentTransform);
        }
        
        SetAllCharacterActive(true);
        string activeCharName = GetSelectedCharacter().gameObject.name;
        //string activeCharName = "J_Slime_01";
        GameObject activeChar = GameObject.Find(activeCharName);
        Transform activeParent = parentTransform;
        activeChar.transform.SetParent(activeParent, true);
        SetAllCharacterActive(false);

    }

    public void SetAllCharacterActive(bool state)
    {
        int childCount = transform.childCount;
        for (int i = 1; i < childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(state);
        }

    }

    public void DeactivateChar(Transform prevChar)
    {

        prevChar = prevChar.GetChild(0);
        prevChar.gameObject.transform.SetParent(this.transform, true);
        prevChar.gameObject.SetActive(false);

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
