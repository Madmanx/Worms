using UnityEngine;

public class CrateComponent : MonoBehaviour
{
    public Renderer inAirRenderer;
    public Renderer onGroundRenderer;

    private CrateType crateType;

    public CrateType CrateType
    {
        get
        {
            return crateType;
        }

        set
        {
            crateType = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Worm"))
        {
            if (collision.collider.GetComponent<WormCharacter>().IsDead)
                return;

            switch (crateType) { 
                case CrateType.Ammo:
                    int rand = Random.Range(0, DatabaseManager.Instance.Db.ListWeapons.Count);
                    WormInventory worm = collision.collider.GetComponent<WormInventory>();

                    for (int i = 0; i < worm.listWeaponInventory.Count; i++)
                    {
                        if (rand == i)
                        {
                            worm.listWeaponInventory[i].NbLeft += Random.Range(1, 3);
                        }
                    }
                    break;
                case CrateType.Heal:
                    WormInfo wormInfo = collision.collider.GetComponent<WormInfo>();
                    wormInfo.Life += 30;
                    break;
            }
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Map"))
        {
            onGroundRenderer.gameObject.SetActive(true);
            inAirRenderer.gameObject.SetActive(false);
        }
    }
}
