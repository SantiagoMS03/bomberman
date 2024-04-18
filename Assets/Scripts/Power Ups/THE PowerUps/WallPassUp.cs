using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/WallPass")]
public class WallPassUp : PowerupEffects
{
    // Increases the bomb's explosion range
    public override void Apply(GameObject target)
    {
        PlayerMovement.Wall_Pass = true;
        Physics2D.IgnoreLayerCollision(7, 8);
    }
}