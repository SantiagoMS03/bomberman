using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/RemoteControl")]
public class RemoteControlUp : PowerupEffects
{
    // Increases the bomb's explosion range
    public override void Apply(GameObject target)
    {
        PlayerMovement.Remote_Control = true;
    }
}
