using UnityEngine;

public class AmmoCollider : MonoBehaviour
{
    private AmmoComponent ownerAmmo;

    public AmmoComponent OwnerAmmo
    {
        get
        {
            return ownerAmmo;
        }

        set
        {
            ownerAmmo = value;
        }
    }

    void Start()
    {
        ownerAmmo = GetComponentInParent<AmmoComponent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Map"))
        {
            // error when throw in water or worm ? ? 
            if (!ownerAmmo)
            {
                if (GetComponentInParent<AmmoComponent>())
                    ownerAmmo = GetComponentInParent<AmmoComponent>();
            }

            // One case for rocket stuck on worm ? 
            ///TODO : Here !!

            ownerAmmo.AmmoBehaviourImpact(collision.collider.GetComponent<TerrainDestructible>());
        }
    }
}
