using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfDamage { Water, Explosion}


// Related to gameloop
public class WormInfo : MonoBehaviour
{
    [SerializeField]
    private int life;

    [SerializeField]
    private TextMeshProUGUI textPseudo;
    [SerializeField]
    private Image colorTeamPseudo;

    [SerializeField]
    private TextMeshProUGUI textLife;
    [SerializeField]
    private Image colorTeamLife;

    [SerializeField]
    private GameObject pointer;

    private string pseudo;
    private Color color;
    private TeamState myTeam;

    private bool possessed;

    public TerrainDestructible underMe;

    #region GET/SET

    public string Pseudo
    {
        get
        {
            return pseudo;
        }

        set
        {
            pseudo = value;
            textPseudo.text = pseudo;
        }
    }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
            if (life > GameManager.Instance.LifeMax) life = GameManager.Instance.LifeMax;
            UpdateLife();
        }
    }


    public bool Possessed
    {
        get
        {
            return possessed;
        }

        set
        {
            if (value == true)
            {
                CameraManager.Instance.MainCameraFollow(transform);
                InventoryManager.Instance.RefreshCurrentWeaponSlot(GetComponent<WormAttack>().CurrentWeaponType);
                GetComponent<WormAttack>().HaveAttackedThisTurn = false;
            }
            possessed = value;
            Pointer.SetActive(possessed);
        }
    }

    public GameObject Pointer
    {
        get
        {
            return pointer;
        }

        set
        {
            pointer = value;
        }
    }

    public Color Color
    {
        get
        {
            return color;
        }

        set
        {
            color = value;
            colorTeamPseudo.color = color;
            colorTeamLife.color = color;
        }
    }

    public TeamState MyTeam
    {
        get
        {
            return myTeam;
        }

        set
        {
            myTeam = value;
        }
    }

    #endregion

    void Start()
    {
        Life = GameManager.Instance.LifeMax;
    }

    private void UpdateLife()
    {
        textLife.text = life.ToString();
    }

    public void WormBehaviourDamage(Vector2 directionFromClosestPoint, int damage)
    {
        GetComponent<Rigidbody2D>().AddForce(Random.Range(3, 5) * (-directionFromClosestPoint + Vector2.up).normalized, ForceMode2D.Impulse);
        ApplyDamage(damage, TypeOfDamage.Explosion);
    }


    public void ApplyDamage(int damage, TypeOfDamage type)
    {
        if (life >= damage)
            Life -= damage;
        else
            Life = 0;

        GameLoopManager.Instance.OnWormTakeDamage(myTeam);
        if (Life == 0)
            DeathEvent(type);

    }

    private void DeathEvent(TypeOfDamage type)
    {
        switch (type)
        {
            case TypeOfDamage.Explosion:
                GetComponent<WormCharacter>().AnimDie();
                Invoke("DeathByExplosion", 1.0f);
                break;
            case TypeOfDamage.Water:
                GetComponent<Rigidbody2D>().drag = 25f;
                GetComponent<WormCharacter>().AnimJump(true);
                Invoke("DeathByWater", 0.1f);
                break;
        }

    }

    public void DeathByWater()
    {

        GetComponent<Collider2D>().enabled = false;
        GetComponent<WormCharacter>().AnimJump(true);

        GameLoopManager.Instance.AskForChangeTurn();
        Invoke("Death", 1.0f);
    }

    public void DeathByExplosion()
    {
        // From Character ? 
        float radiusOnDeath = 0.25f;
        int damage = 25;

        // Propagation of explosion
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusOnDeath, (1 << LayerMask.NameToLayer("Worm")));
        foreach (Collider2D collider in colliders)
        {
            if (collider)
            {
                collider.GetComponent<WormInfo>().WormBehaviourDamage((transform.position - collider.transform.position).normalized, damage);
            }
        }
        GameObject go = Instantiate(DatabaseManager.Instance.Db.Explosion, transform.position, Quaternion.identity, null);
        go.transform.localScale *= radiusOnDeath;
        underMe.DestroyGround(transform.position, radiusOnDeath);

        Invoke("Death", 0.5f);
    }

    public void Death()
    {
        GameLoopManager.Instance.OnWormDeath(this);
        InventoryManager.Instance.ClearInventoryContainer();

        // No anim so destroy
        GetComponent<WormCharacter>().AnimDead();

    }
}
