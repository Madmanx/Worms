using UnityEngine;

public class Rocket : AmmoComponent
{
    public override void AmmoBehaviourFire()
    {
        Rb.AddForce(Direction * Force, ForceMode2D.Impulse);

        // In case of bug
        Destroy(gameObject, 10.0f);
    }

}
