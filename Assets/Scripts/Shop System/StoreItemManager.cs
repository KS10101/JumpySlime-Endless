using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemManager : MonoBehaviour
{
    public static StoreItemManager instance;
    public List<StoreItemData> ItemList;
    [SerializeField] private GameObject StoreItemPrefab;
    [SerializeField] private Transform ItemContainer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        GenerateStoreItems();
    }

    public void GenerateStoreItems()
    {
        DeleteGeneratedItems();

        for (int i = 0; i < ItemList.Count; i++)
        {
            GameObject item = Instantiate(StoreItemPrefab, ItemContainer);
            item.GetComponent<ShopItem>().Init(ItemList[i]);
        }
    }

    public void DeleteGeneratedItems()
    {
        int ItemNum = ItemContainer.childCount;

        if (ItemNum == 0) return;

        for (int i = 0; i < ItemNum; i++)
        {
            Destroy(ItemContainer.GetChild(i).gameObject);
        }
    }
}
