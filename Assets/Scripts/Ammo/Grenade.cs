using UnityEngine;

public class Grenade : AmmoComponent
{
    private TerrainDestructible terrain;

    public override void AmmoBehaviourFire()
    {
        Rb.AddForce(Direction * Force, ForceMode2D.Impulse);

        Invoke("ExplodeAfterDelay", 2);

        // In case of bug
        Destroy(gameObject, 10.0f);
    }

    public override void AmmoBehaviourImpact(TerrainDestructible _terrain)
    {
        terrain = _terrain;
    }

    public void ExplodeAfterDelay()
    {
         base.AmmoBehaviourImpact(terrain);
    }
}
