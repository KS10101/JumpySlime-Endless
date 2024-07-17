using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Store Item Data 1" ,menuName = "Store Item Data")]
public class StoreItemData : ScriptableObject
{
    public Sprite IconSprite;
    public int NumberOfCoins;
    public float price;
}
