using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Worm"))
        {
            if (collider.GetComponent<WormCharacter>().IsDead)
                return;

            collider.GetComponent<WormInfo>().ApplyDamage(100, TypeOfDamage.Water);
        }
        else
        {
            if (collider.GetComponentInParent<AmmoComponent>())
            {
                collider.GetComponentInParent<Rigidbody2D>().drag = 25;
                Invoke("ChangeFocus", 1f);
            }
        }
    }

    public void ChangeFocus()
    {
        CameraManager.Instance.MainCameraFollow(GameLoopManager.Instance.GetActiveWorm().transform);
    }
}
