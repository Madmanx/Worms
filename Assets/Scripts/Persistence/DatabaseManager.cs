using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    private static DatabaseManager singleton;

    private Database db;

    public static DatabaseManager Instance
    {
        get
        {
            return singleton;
        }

        set
        {
             singleton = value;
        }
    }

    public Database Db
    {
        get
        {
            if (!db ) db = Resources.Load<Database>("Database") as Database;
            return db;
        }
    }

    public void Awake()
    {
        singleton = this;
    }

    public void Fire(WormCharacter shooter, WeaponType typeWeapon, Vector2 startPos, Vector2 dir, float forceFactor = 1)
    {
        GameObject ammo = Instantiate(db.ListWeapons[(int)typeWeapon - 1].prefab);
        AmmoComponent ammoComponent = ammo.GetComponent<AmmoComponent>();

        Physics2D.IgnoreCollision(shooter.GetComponent<Collider2D>(), ammoComponent.AmmoCollider);
        ammoComponent.Direction = dir;
        ammoComponent.DamageRadius = db.ListWeapons[(int)typeWeapon - 1].damageRadius;
        ammoComponent.Damage = db.ListWeapons[(int)typeWeapon - 1].damage;
        ammoComponent.Force = db.ListWeapons[(int)typeWeapon - 1].force * forceFactor;

        ammo.transform.position = startPos;
    }

    public void SpawnCrate(CrateType typeCrate, Vector2 startPos)
    {
        GameObject crate = Instantiate(db.ListCrate[(int)typeCrate - 1].prefab);
        CrateComponent crateComponent = crate.GetComponent<CrateComponent>();
        crateComponent.CrateType = typeCrate;
        crate.transform.position = startPos;
    }
}
