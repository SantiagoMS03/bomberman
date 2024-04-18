using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public bool timer;
    public float Timeleft = 0.5f;
    public int pointsValue = 100;
    public Rigidbody2D Hitbox;
    public PowerupEffects Effects;
    public ScoreDisplay scoreText;

    void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<ScoreDisplay>();
    }


    void Update()
    {
        // Invincibility Timer, so the item won't be destroyed instantly
        if (timer)
        {
            Timeleft -= Time.deltaTime;
            if (Timeleft <= 0)
            {
                Hitbox.simulated = true;
                timer = false;
            }
        }

    }

    // Detects & reacts to gameobjects with specific tags
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            scoreText.AddPoints(pointsValue);
            Effects.Apply(collision.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }
    }
}
