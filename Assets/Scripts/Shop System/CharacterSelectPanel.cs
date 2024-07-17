using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanel : ItemsPanel
{
    public static CharacterSelectPanel instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public override void CloseStore()
    {
        base.CloseStore();
        CharacterManager.instance.SetCharacterState();
        CharacterManager.instance.UpdateScriptableListDataState();

    }
}
