using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The main control for not only player movement but also bomb placement and potentially more
public class PlayerMovement : MonoBehaviour
{
    private Transform Player_Pos;
    [Header("Booleans")]
    public bool CanPlaceBomb = true;
    public bool Lay_Bomb;
    private bool Stop = true;
    [Space(2f)]
    [Header("Bombs and Cooldown")]
    public int Bombs_Dropped;
    public int Bombs_Maxed;
    public float Cooldown;
    public float Cooldown_OG;
    public GameObject Spawn_Bombs;
    public BombController BombEffects;
    [Space(2f)]
    [Header("Player Movement")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput; 
    Animator Player_Anim;

    void Start()
    {
        Player_Pos = gameObject.GetComponent<Transform>();
        BombEffects = Spawn_Bombs.GetComponent<BombController>();
        Player_Anim = GetComponent<Animator>();
        BombEffects.Firepower_Setter = 0;
    }

    void Update()
    {
        // Allows the player to place a bomb (Used to fix a bug)
        if (CanPlaceBomb == false && Lay_Bomb == false && Cooldown == Cooldown_OG)
        {
            CanPlaceBomb = true;
        }

        // Player input movement
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        
        // Player animation
        if (PlayerInput != Vector2.zero)
        {
            Player_Anim.SetBool("isWalking", true);
            Player_Anim.SetFloat("input_x", PlayerInput.x);
            Player_Anim.SetFloat("input_y", PlayerInput.normalized.y);
        }
        else
        {
            Player_Anim.SetBool("isWalking", false);
        }
        
        // Bomb placement 
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            if (CanPlaceBomb)
            {
                Lay_Bomb = true;
                if (Stop)
                {
                    // Stop spawning bombs if the total amount of bomb placement has reached its max
                    if (Bombs_Dropped < Bombs_Maxed)
                    {
                        Instantiate(Spawn_Bombs, Player_Pos.position, Player_Pos.rotation);
                        Bombs_Dropped++;
                    }
                    else
                    {
                        Stop = false;
                    }

                }
            }

        }

        // Bomb Cooldown timer that will reset the total amount of bombs placed
        if (Lay_Bomb)
        {
            Cooldown -= Time.deltaTime;
            if (Cooldown <= 0)
            {
                Stop = true;
                Lay_Bomb = false;
                Cooldown = Cooldown_OG;
                Bombs_Dropped = 0;
            }
        }

    }

    // Updates the player's movement & speed by using it's Rigidbody
    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        rb.velocity = moveForce;
    }


    // All 3 of these triggers prevent the player from placing a bomb on top of another bomb
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = false;
        }
    }

   
}
