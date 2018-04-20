using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour {

    private WormCharacter wormCharacter;
    private WormInventory wormInventory;
    private WormAttack wormAttack;
    private Direction dir;

    public GameObject prefabChargementRocketType;
    public GameObject goChargementRocketType;
    private bool needReclick = false;

    private void Start()
    {
        wormCharacter = GetComponent<WormCharacter>();
        wormInventory = GetComponent<WormInventory>();
        wormAttack = GetComponent<WormAttack>();
    }

    // Update is called once per frame
    void Update() {

        wormCharacter.AnimJump(!wormCharacter.onGround);


        // if not possessed return + chargement rocket test
        if (!GetComponent<WormInfo>().Possessed)
        {
            if (goChargementRocketType != null)
            {
                Destroy(goChargementRocketType);
            }

            wormCharacter.AnimWalk(Input.anyKey);
            return;
        }

        ///TODO : Here !!
        // chargement rocket angles test
        //if( needReclick && goChargementRocketType)
        //{
        //    goChargementRocketType.transform.rotation = Quaternion.Euler(new Vector3(0, 0, CalculateDirectionFromMouse().x));
        //}

        // Controls Inventory
        if (Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(1))
        {
            // Chargement Rocket control
            if (needReclick)
                return;

            wormInventory.IsInventoryOpen = !wormInventory.IsInventoryOpen;
            wormInventory.ToogleInventory(wormInventory.IsInventoryOpen);
        }

        if (GetComponent<WormInventory>().IsInventoryOpen)
            return;
        
        // Controls Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (wormAttack.HaveAttackedThisTurn)
                return; 

            // Could be copied into attack behavior
            if (wormAttack.CurrentWeaponType == WeaponType.Rocket)
            {
                ///TODO : Here !!
                // Chargement rocket
                if (!needReclick)
                {
                    needReclick = true;
                    goChargementRocketType = Instantiate(prefabChargementRocketType, null);
                } else
                {
                    wormAttack.Fire(CalculateDirectionFromMouse(), goChargementRocketType.GetComponent<ChargementRocket>().Value);
                    Destroy(goChargementRocketType);
                    needReclick = false;
                }
            }
            else
            {
                wormAttack.Fire(CalculateDirectionFromMouse());
            }
        }

        if (needReclick)
            return;

        ///TODO : Here !!
        ///Cas exceptionel ou le raycast ne detect pas le sol ( faire un sphere cast )
        if (Input.GetKeyDown(KeyCode.Space) && wormCharacter.onGround)
            wormCharacter.Jump();
        else
        {
            dir = Direction.Stop;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
                dir = Direction.Left;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                dir = Direction.Right;

            if (wormCharacter.onGround)
                wormCharacter.Movement(dir);
            else
                wormCharacter.MovementInAir(dir);
        }  
    }

    private Vector2 CalculateDirectionFromMouse()
    {
        return ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized);
    }

}
