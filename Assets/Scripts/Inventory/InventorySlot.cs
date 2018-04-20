using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    WormInventory inventoryOwner;
    WeaponType weaponType;

    #region GET/SET

    public WormInventory InventoryOwner
    {
        get
        {
            return inventoryOwner;
        }

        set
        {
            inventoryOwner = value;
        }
    }

    public WeaponType WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }

    #endregion


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!inventoryOwner) return;

        if (inventoryOwner.listWeaponInventory.Find(a => a.WeaponType == weaponType).NbLeft == 0)
            inventoryOwner.GetComponent<WormAttack>().CurrentWeaponType = WeaponType.None;
        else
            inventoryOwner.GetComponent<WormAttack>().CurrentWeaponType = weaponType;
    }
}
