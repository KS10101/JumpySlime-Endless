using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanel : ItemsPanel
{
    public static StorePanel instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public override void CloseStore()
    {
        base.CloseStore();

    }

}
