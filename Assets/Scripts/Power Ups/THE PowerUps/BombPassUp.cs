using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BombPass")]
public class BombPassUp : PowerupEffects
{
    // Increases the bomb's explosion range
    public override void Apply(GameObject target)
    {
        PlayerMovement.Bomb_Pass = true;
    }
}
