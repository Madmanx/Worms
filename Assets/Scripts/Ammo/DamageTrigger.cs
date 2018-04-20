using UnityEngine;


// ShouldDesactivate
public class DamageTrigger : MonoBehaviour
{ 
    private AmmoComponent ownerAmmo;

    public bool damageApplied = false;

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
        GetComponent<CircleCollider2D>().radius = ownerAmmo.DamageRadius;
    }

    public void OnEnable()
    {
        if (!damageApplied)
        {
            // Fix de bug Dynamite ne veut pas déclencher son trigger???
            GetComponent<CircleCollider2D>().radius = ownerAmmo.DamageRadius;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ownerAmmo.DamageRadius, (1 << LayerMask.NameToLayer("Worm")));
            foreach (Collider2D collider in colliders)
            {
                if (collider && ownerAmmo)
                {
                    ownerAmmo.AmmoBehaviourDamage((transform.position - collider.transform.position).normalized, collider.GetComponent<WormInfo>());
                    damageApplied = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Worm"))
        {
            if (!ownerAmmo)
            {
                if (GetComponentInParent<AmmoComponent>())
                    ownerAmmo = GetComponentInParent<AmmoComponent>();
            }

            // For now always of type Explosion for Ammo
            if (!damageApplied)
            {
            //    damageApplied = true; // Commenter sinon sa empeche les autres cas standard ( grenade)
                ownerAmmo.AmmoBehaviourDamage((transform.position - collider.transform.position).normalized, collider.GetComponent<WormInfo>());
            }

        }
    }
}
