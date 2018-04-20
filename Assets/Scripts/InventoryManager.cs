using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public Transform InventoryContainer;
    public Transform CurrentWeaponSlot;

    public GameObject prefabNbUseTextGo;
    public GameObject prefabInventorySlot;

    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    public void RefreshCurrentWeaponSlot(WeaponType t)
    {
        int childs = CurrentWeaponSlot.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(CurrentWeaponSlot.transform.GetChild(i).gameObject);
        }

        if( t != WeaponType.None)
        {
            GameObject weapon = Instantiate(DatabaseManager.Instance.Db.ListWeapons.Find(a => a.weaponType == t).sprite, CurrentWeaponSlot.transform);
            weapon.transform.localPosition = Vector2.zero;
            weapon.transform.localScale *= 1.3f;
        }
    }

    public void ClearInventoryContainer()
    {
        int childs = InventoryContainer.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(InventoryContainer.transform.GetChild(i).gameObject);
        }
        InventoryContainer.gameObject.SetActive(false);
    }
}
