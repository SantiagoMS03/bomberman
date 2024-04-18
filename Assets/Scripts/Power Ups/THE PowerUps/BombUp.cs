using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BombUp")]
public class BombUp : PowerupEffects
{
    public override void Apply(GameObject target)
    {
        PlayerMovement.Bombs_Maxed += 1;
    }
}
