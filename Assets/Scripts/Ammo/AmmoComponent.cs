using UnityEngine;

public class AmmoComponent : MonoBehaviour
{
    private Rigidbody2D rb;

    public Collider2D AmmoCollider;

    [SerializeField]
    private DamageTrigger damageRadiusTrigger;

    private float damageRadius;

    private Vector2 direction;
    private float force;
    private int damage;

    #region GET/SET

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float Force
    {
        get
        {
            return force;
        }

        set
        {
            force = value;
        }
    }

    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }

        set
        {
            rb = value;
        }
    }

    public float DamageRadius
    {
        get
        {
            return damageRadius;
        }

        set
        {
            damageRadius = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        // To be sure
        AmmoCollider.GetComponent<AmmoCollider>().OwnerAmmo = this;
        damageRadiusTrigger.OwnerAmmo = this;


        CameraManager.Instance.MainCameraFollow(transform);

        AmmoBehaviourFire();
    }

    public virtual void AmmoBehaviourFire() { }


    public virtual void AmmoBehaviourImpact(TerrainDestructible terrain)
    {
        OnExplode();

        if( terrain)
        terrain.DestroyGround(transform.position, DamageRadius);
    }


    public virtual void AmmoBehaviourDamage(Vector2 directionFromClosestPoint, WormInfo toDamage)
    {
        toDamage.WormBehaviourDamage(directionFromClosestPoint, Damage);
    }

    public void OnExplode()
    {
        damageRadiusTrigger.gameObject.SetActive(true);

        GameObject go = Instantiate(DatabaseManager.Instance.Db.Explosion, transform.position, Quaternion.identity, null);
        go.transform.localScale *= damageRadius;

        // Maybe synchronise destructions 
        Destroy(gameObject, 0.1f);

        GameLoopManager.Instance.AskForChangeTurn();
    }
}
