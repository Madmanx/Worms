using TMPro;
using UnityEngine;

public class Dynamite : AmmoComponent
{
    private TerrainDestructible terrain;

    public TextMeshProUGUI textTimer;
    public float timerDuration;

    private float timer;
    public bool timerOver = false;
    public bool activate = false;

    public override void AmmoBehaviourFire()
    {
        timer = timerDuration;
        activate = true;

        // Negligeable mais je le laisse
        Rb.AddForce(Direction * Force, ForceMode2D.Impulse);

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

    public void Update()
    {
        if (!activate)
            return;

        if (timerOver == false)
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                textTimer.text = ((int)timer).ToString();
            }
            else
                timerOver = true;
        }
        else
            ExplodeAfterDelay();

    }
}
