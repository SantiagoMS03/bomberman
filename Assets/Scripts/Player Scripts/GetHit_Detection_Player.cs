using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit_Detection_Player : MonoBehaviour
{
    public bool Hit;
    public PlayerMovement Player;
    // Detects & reacts to gameobjects with specific tags
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!Player.FireProof_Status || !Player.Invincible)
        {
            if (collision.CompareTag("Explosion") || collision.CompareTag("Enemies") || collision.CompareTag("Explosion2"))
            {
                Player.Dying = true;
                Hit = true;
            }
        }
       
    }
}
