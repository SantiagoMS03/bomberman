using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private BoxCollider2D Box;
    [Header("Booleans")]
    public bool Activate;
    public bool Timer;
    private bool tick = true;
    public bool[] Stop_Spawning;
    [Space(2f)]
    [Header("Explosion Setup")]
    public GameObject The_Explosion_Set;
    public GameObject The_Explosion_Spawn;
    [Space(2f)]
    [Header("Explosion Parent Directions")]
    public GameObject[] Parents;
    public Transform[] The_Parents;
    public Vector2[] Parent_vectors;
    [Space(2f)]
    [Header("Firepower and Trackers")]
    public int Firepower;
    public int Firepower_Setter;
    public float Detonator_Timer = 2;
    private float timer2 = 0.2f;
    public int[] Total_Amount_Tracker;
    private float X_Distance = 1;
    private float Y_Distance = 1;
    [Space(2f)]
    [Header("Explosion Range Detection")]
    public GameObject[] The_Hitbox_Detection;
    public Wall_Detection[] Hitbox_Detection;


    // Start is called before the first frame update
    void Awake()
    {
        Box = gameObject.GetComponent<BoxCollider2D>(); ;
        Firepower = Firepower_Setter;
        // Top Explosion
        Parent_vectors[0] = new Vector2(The_Parents[0].position.x, The_Parents[0].position.y + Y_Distance);
        // Right Explosion
        Parent_vectors[1] = new Vector2(The_Parents[1].position.x + X_Distance, The_Parents[1].position.y);
        // Bottom Explosion
        Parent_vectors[2] = new Vector2(The_Parents[2].position.x, The_Parents[2].position.y - Y_Distance);
        // Left Explosion
        Parent_vectors[3] = new Vector2(The_Parents[3].position.x - X_Distance, The_Parents[3].position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Stop increasing explosion range if the hitboxes have collided with a wall or destroyable wall
        if (Hitbox_Detection[0].Hit || Hitbox_Detection[0].Hit2)
        {
            Stop_Spawning[0] = true;
        }
        if (Hitbox_Detection[1].Hit || Hitbox_Detection[1].Hit2)
        {
            Stop_Spawning[1] = true;
        }
        if (Hitbox_Detection[2].Hit || Hitbox_Detection[2].Hit2)
        {
            Stop_Spawning[2] = true;
        }
        if (Hitbox_Detection[3].Hit || Hitbox_Detection[3].Hit2)
        {
            Stop_Spawning[3] = true;
        }

        // Starts up the explosion range setup  
        if (tick)
        {
            timer2 = Time.deltaTime;
            if (Detonator_Timer <= 0)
            {
                Activate = true;
                tick = false;
            }
        }

        if (Activate)
        {
            // Increases the explosion range for all 4 sides 
            for (int i = 0; i < Firepower; i++)
            {
                // Top Explosion side
                if (Stop_Spawning[0] == false)
                {
                    (Instantiate(The_Explosion_Spawn, Parent_vectors[0], The_Parents[0].rotation) as GameObject).transform.parent = The_Parents[0].transform;
                    The_Hitbox_Detection[0].GetComponent<Transform>().position = Parent_vectors[0];
                    Parent_vectors[0].y += Y_Distance;
                    Total_Amount_Tracker[0]++;
                }

                // Right Explosion side
                if (Stop_Spawning[1] == false)
                {
                    (Instantiate(The_Explosion_Spawn, Parent_vectors[1], The_Parents[1].rotation) as GameObject).transform.parent = The_Parents[1].transform;
                    The_Hitbox_Detection[1].GetComponent<Transform>().position = Parent_vectors[1];
                    Parent_vectors[1].x += X_Distance;
                    Total_Amount_Tracker[1]++;
                }

                // Bottom Explosion side
                if (Stop_Spawning[2] == false)
                {
                    (Instantiate(The_Explosion_Spawn, Parent_vectors[2], The_Parents[2].rotation) as GameObject).transform.parent = The_Parents[2].transform;
                    The_Hitbox_Detection[2].GetComponent<Transform>().position = Parent_vectors[2];
                    Parent_vectors[2].y -= Y_Distance;
                    Total_Amount_Tracker[2]++;
                }

                // Left Explosion side
                if (Stop_Spawning[3] == false)
                {
                    (Instantiate(The_Explosion_Spawn, Parent_vectors[3], The_Parents[3].rotation) as GameObject).transform.parent = The_Parents[3].transform;
                    The_Hitbox_Detection[3].GetComponent<Transform>().position = Parent_vectors[3];
                    Parent_vectors[3].x -= X_Distance;
                    Total_Amount_Tracker[3]++;
                }
            }
            Activate = false;
        }

        // The cutoff point for when an explosion reaches a wall or a destroyable wall (prevents the explosion from going through walls)
        for (int i = 0; i < 3; i++)
        {
            if (Firepower > 1)
            {
                if (Total_Amount_Tracker[i] == 0 && Hitbox_Detection[i].Hit2 == false)
                {
                    Parents[i].GetComponent<Rigidbody2D>().simulated = false;
                    Parents[i].SetActive(false);
                }
            }
            else
            {
                if (Hitbox_Detection[i].Hit)
                {
                    Parents[i].GetComponent<Rigidbody2D>().simulated = false;
                    Parents[i].SetActive(false);
                }
            }

        }

        // Timer when the explosion happens
        if (Timer)
        {
            Detonator_Timer -= Time.deltaTime;
            if (Detonator_Timer <= 0)
            {
                The_Explosion_Set.SetActive(true);
                Timer = false;
                Box.isTrigger = true;
            }
        }
    }

    // Allows the player to stand inside the bomb when placed, but when they move of its hitbox they can't go through it anymore
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Timer)
            {
                Box.isTrigger = false;
            }

        }
    }

    // Detonates instantly when colliding with an explosion
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Detonator_Timer = 0;
        }
    }

}
