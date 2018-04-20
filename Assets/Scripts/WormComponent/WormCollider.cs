using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollider : MonoBehaviour
{
    WormCharacter wormCharacter;
    WormInfo wormState;

    RaycastHit2D hit;

    public float distance = 0.1f;
    public LayerMask layerConsiderAsGround;

    private void Start()
    {
        wormCharacter = GetComponent<WormCharacter>();
        wormState = GetComponent<WormInfo>();
    }

    public void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, distance, layerConsiderAsGround);
        if (wormCharacter.onGround = hit)
        {
            if (hit.collider.CompareTag("Map"))
            {
                wormState.underMe = hit.collider.GetComponent<TerrainDestructible>();
            }
        }
    }
}
