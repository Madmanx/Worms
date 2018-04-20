using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAttack : MonoBehaviour
{
    private WormInventory wormInventory;
    private WormCharacter wormInfo;

    private bool haveAttackedThisTurn =false;

    [SerializeField]
    private WeaponType currentWeaponType;
    public WeaponType CurrentWeaponType
    {
        get
        {
            return currentWeaponType;
        }
        set
        {
            currentWeaponType = value;
            InventoryManager.Instance.RefreshCurrentWeaponSlot(currentWeaponType);
        }
    }

    public bool HaveAttackedThisTurn
    {
        get
        {
            return haveAttackedThisTurn;
        }

        set
        {
            haveAttackedThisTurn = value;
        }
    }

    private void Start()
    {
        wormInventory = GetComponent<WormInventory>();
        wormInfo = GetComponent<WormCharacter>();
    }

    public void Fire(Vector2 dir, float forceFactor = 1)
    {
        if (CurrentWeaponType != WeaponType.None)
        {
            DatabaseManager.Instance.Fire(wormInfo, CurrentWeaponType, transform.position, dir, forceFactor);
            haveAttackedThisTurn = true;

            if ( wormInventory.RemoveAmmo(CurrentWeaponType) == false)
            {
                CurrentWeaponType = WeaponType.None;
            }
        }
    }
}
