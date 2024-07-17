using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public List<CharacterData> CharList;
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform storeItemContainer;
    private List<CharacterState> charStateList = new List<CharacterState>();
    
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

        UpdateCharStateList();
    }

    // creates the Item cards in store UI panel and set data from scriptable
    public void InitiateStoreItems()
    {
        DeleteGeneratedItems();

        for(int i=0 ; i<CharList.Count ; i++)
        {
            GameObject item = Instantiate(storeItemPrefab, storeItemContainer);
            item.GetComponent<CharStoreItem>().Init(CharList[i]);
        }
    }

    public void UpdateScriptableListDataState()
    {
        for(int i = 0; i < CharList.Count; i++)
        {
            if (charStateList[i].charID == CharList[i].CharID)
            {
                CharList[i].SetLock(charStateList[i].isUnlocked);
                CharList[i].SetSelect(charStateList[i].isSelected);
            }
        }
    }

    private void HandleDataMismatch()
    {
        if(CharList.Count > charStateList.Count)
        {
            int diff = CharList.Count - charStateList.Count;
            for(int i = charStateList.Count; i < CharList.Count; i++)
            {
                if (CharList[i] != null)
                {
                    CharacterState charState = new CharacterState
                    {
                        charID = CharList[i].CharID,
                        isUnlocked = CharList[i].isUnlocked,
                        isSelected = CharList[i].isSelected
                    };

                    charStateList.Add(charState);
                }
            }
        }
        SaveToJSON();
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

        InitiateStoreItems();
        //SaveToJSON();
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
            DeactivateChar(parentTransform);
        
        SetAllCharacterActive(true);
        string activeCharName = GetSelectedCharacter();
        //string activeCharName = "J_Slime_01";
        GameObject activeChar = GameObject.Find(activeCharName);
        Transform activeParent = parentTransform;
        activeChar.transform.SetParent(activeParent);
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
        prevChar.gameObject.transform.SetParent(this.transform);
        prevChar.gameObject.SetActive(false);

    }

    public  string GetSelectedCharacter()
    {
        string CharPrefab = "";
        for (int i = 0; i < CharList.Count; i++)
        {
            if (CharList[i].isSelected)
            {
                CharPrefab = CharList[i].characterPrefab.gameObject.name;
                break;
            }
        }

        return CharPrefab;
    }

    public void SetCharacterState()
    {
        charStateList.Clear();
        for (int i = 0; i < CharList.Count; i++)
        {
            CharacterState charState = new CharacterState
            {
                charID = CharList[i].CharID,
                isUnlocked = CharList[i].isUnlocked,
                isSelected = CharList[i].isSelected
            };

            charStateList.Add(charState);
        }

        SaveToJSON();
        //GetCharacterState();
    }

    public void GetCharacterState(CharacterStateList stateList)
    {
        this.charStateList = stateList.CharStateList;
    }

    public void SaveToJSON()
    {
        PlayerPrefs.DeleteKey("CharacterStates");
        CharacterStateList StateList = new CharacterStateList { CharStateList = this.charStateList };
        string json = JsonUtility.ToJson(StateList);
        PlayerPrefs.SetString("CharacterStates", json);
        PlayerPrefs.Save();
        Debug.Log($"Saved to JSon" +
            $"{PlayerPrefs.GetString("CharacterStates")}");
    }

    public CharacterStateList LoadFromJSON()
    {
        if (!PlayerPrefs.HasKey("CharacterStates") || 
            PlayerPrefs.GetString("CharacterStates") == null) return null;

        string json = PlayerPrefs.GetString("CharacterStates");
        CharacterStateList stateList = JsonUtility.FromJson<CharacterStateList>(json);

        return stateList;
    }

    public void UpdateCharStateList()
    {
        CharacterStateList statelist = LoadFromJSON();
        
        if(statelist == null)
        {
            SetCharacterState();
        }
        else
        {
            GetCharacterState(statelist);
            HandleDataMismatch();
            UpdateScriptableListDataState();
        }
    }
    


#if UNITY_EDITOR
    [ContextMenu("Cache Char. prefab")]
    public void CreateChildPrefabs()
    {
        DestroyPrefabs();

        for (int i = 0; i < CharList.Count; i++)
        {
            GameObject prfab = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(CharList[i].characterPrefab);
            prfab.transform.parent = this.transform;
            prfab.SetActive(false);
        }

    }

    [ContextMenu("Destroy Cache")]
    public void DestroyPrefabs()
    {
        int count = this.transform.childCount;
        Debug.Log(count);
        if (count <= 1) return;

        while (count > 1)
        {
            if (this.transform.GetChild(1) != null)
            {
                DestroyImmediate(this.transform.GetChild(1).gameObject);

            }
            count--;
        }
    }
#endif
}

public class CharacterStateList
{
    public List<CharacterState> CharStateList;
}

[System.Serializable]
public class CharacterState
{
    public int charID;
    public bool isUnlocked;
    public bool isSelected;
}
