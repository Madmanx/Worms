using UnityEngine;
using System.Collections.Generic;

public enum CrateType { None, Heal, Ammo }

public enum WeaponType { None, Grenade, Rocket, Dynamite, Holy}

[System.Serializable]
public class Crate
{
    [SerializeField]
    public CrateType crateType;

    [SerializeField]
    public GameObject prefab;

    public Crate(GameObject _prefab, CrateType _type)
    {
        prefab = _prefab;
        crateType = _type;
    }
}

[System.Serializable]
public class Weapon
{
    [SerializeField]
    public WeaponType weaponType;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    public float damageRadius = 0.25f;

    [SerializeField]
    [Range(0, 100)]
    public int damage = 20;

    [SerializeField]
    [Range(1, 40)]
    public float force = 10.0f;

    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public GameObject sprite;

    public override string ToString()
    {
        return weaponType.ToString();
    }

    public Weapon(WeaponType _type, float _damageRadius, int _damage, float _force, GameObject _prefab, GameObject _sprite)
    {
        weaponType = _type;
        damageRadius = _damageRadius;
        damage = _damage;
        force = _force;
        prefab = _prefab;
        sprite = _sprite;
    }

}

[CreateAssetMenu(fileName = "Database", menuName = "Database", order = 1)]
public class Database : ScriptableObject
{
    [SerializeField]
    private List<Weapon> listWeapons = new List<Weapon>();
    [SerializeField]
    private List<Crate> listCrate = new List<Crate>();
    [SerializeField]
    private GameObject explosion;

    public List<Weapon> ListWeapons
    {
        get
        {
            return listWeapons;
        }

        set
        {
            listWeapons = value;
        }
    }

    public List<Crate> ListCrate
    {
        get
        {
            return listCrate;
        }

        set
        {
            listCrate = value;
        }
    }

    public GameObject Explosion
    {
        get
        {
            return explosion;
        }

        set
        {
            explosion = value;
        }
    }

    public void ResetAll()
    {
        listWeapons.Clear();
        listCrate.Clear();

        explosion = Resources.Load<GameObject>("ExplosionTypeA");

        listWeapons.Add(new Weapon(WeaponType.Grenade, 0.35f, 40, 25, Resources.Load<GameObject>("Grenade") as GameObject, Resources.Load<GameObject>("GrenadeIcon") as GameObject));
        listWeapons.Add(new Weapon(WeaponType.Rocket, 0.25f, 30, 40, Resources.Load<GameObject>("Rocket") as GameObject, Resources.Load<GameObject>("RocketIcon") as GameObject));
        listWeapons.Add(new Weapon(WeaponType.Dynamite, 0.6f, 50, 1, Resources.Load<GameObject>("Dynamite") as GameObject, Resources.Load<GameObject>("DynamiteIcon") as GameObject));
        listWeapons.Add(new Weapon(WeaponType.Holy, 0.8f, 100, 20, Resources.Load<GameObject>("Grenade") as GameObject, Resources.Load<GameObject>("HolyIcon") as GameObject));

        listCrate.Add(new Crate(Resources.Load<GameObject>("HealCrate") as GameObject, CrateType.Heal));
        listCrate.Add(new Crate(Resources.Load<GameObject>("AmmoCrate") as GameObject, CrateType.Ammo));
    }
}
