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


}
