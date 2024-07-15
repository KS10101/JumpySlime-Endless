using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InitOnEdit : MonoBehaviour
{
    public List<CharacterData> CharList;
    public bool createPrefabs = false;

#if UNITY_EDITOR
    [ContextMenu("Cache Char. prefab")]
    public void CreateChildPrefabs()
    {
        DestroyPrefabs();
        if (createPrefabs)
        {
            for (int i = 0; i < CharList.Count; i++)
            {
                GameObject prfab = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(CharList[i].characterPrefab);
                prfab.transform.parent = this.transform;
                prfab.SetActive(false);
            }
        }
    }

    [ContextMenu("Destroy Cache")]
    public void DestroyPrefabs()
    {
        int count = this.transform.childCount;
        Debug.Log(count);
        if (count <= 1) return;

        while (count > 0)
        {
            if(this.transform.GetChild(1) != null)
            {
                DestroyImmediate(this.transform.GetChild(1).gameObject);

            }
            count--;
        }
    } 
#endif

}
