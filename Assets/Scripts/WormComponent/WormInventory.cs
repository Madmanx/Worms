using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class WormInventory : MonoBehaviour
{
    private GameObject inventorySlot;
    private GameObject inventoryTextNbUse;

    [SerializeField]
    public List<WeaponInventory> listWeaponInventory;

    private bool isInventoryOpen;
    private GameObject inventoryPanel;

    public bool IsInventoryOpen
    {
        get
        {
            return isInventoryOpen;
        }
        set
        {
            isInventoryOpen = value;
        }
    }

    private void Start()
    {
        isInventoryOpen = false;
        inventoryPanel = InventoryManager.Instance.InventoryContainer.gameObject;
        inventoryPanel.SetActive(false);

        inventorySlot = InventoryManager.Instance.prefabInventorySlot;
        inventoryTextNbUse = InventoryManager.Instance.prefabNbUseTextGo;
    }

    public bool RemoveAmmo(WeaponType weaponType)
    {
        listWeaponInventory[(int)weaponType-1].NbLeft -= 1;
        if (listWeaponInventory[(int)weaponType - 1].NbLeft == 0) return false;
        return true;
    }
    public void ToogleInventory(bool isInventoryOpen)
    {
        if (isInventoryOpen)
            ConstructInventoryPanel();
        else
            ClearInventoryPanel();
    }
    private void ConstructInventoryPanel()
    {
        int line = -1;
        int column = 0;
        for (int i = 0; i < listWeaponInventory.Count; i++)
        {
            if (i % 3 == 0)
            {
                line++;
                column = 0;
            }
            Vector2 pos = new Vector2(-90 + (column * 100), 200 - (line * 100));

            GameObject slot = Instantiate(inventorySlot, inventoryPanel.transform);
            slot.transform.localPosition = pos;
            slot.GetComponent<InventorySlot>().InventoryOwner = this;
            slot.GetComponent<InventorySlot>().WeaponType = listWeaponInventory[i].WeaponType;

            GameObject weapon = Instantiate(DatabaseManager.Instance.Db.ListWeapons.Find(a => a.ToString() == listWeaponInventory[i].WeaponType.ToString()).sprite, slot.transform);
            weapon.transform.localPosition = Vector2.zero;

            GameObject numberUse = Instantiate(inventoryTextNbUse, slot.transform);
            numberUse.GetComponent<TextMeshProUGUI>().text = "x" + listWeaponInventory[i].NbLeft;
            column++;
        }

        inventoryPanel.SetActive(true);

    }
    private void ClearInventoryPanel()
    {
        InventoryManager.Instance.ClearInventoryContainer();
    }
}

[Serializable]
public class WeaponInventory
{
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private int nbLeft;

    public WeaponType WeaponType
    {
        get
        {
            return weaponType;
        }
    }

    public int NbLeft
    {
        get
        {
            return nbLeft;
        }

        set
        {
            nbLeft = value;
            if (nbLeft < 0) nbLeft = 0;
        }
    }
}
