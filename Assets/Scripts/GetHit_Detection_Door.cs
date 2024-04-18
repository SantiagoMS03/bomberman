using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GetHit_Detection_Door : MonoBehaviour
{
    public bool Complete;
    public bool timer;
    public float Timeleft = 0.5f;
    public float timer_Load;
    public ExitDoor_Control Door;
    public Rigidbody2D Hitbox;
    public AudioSource Complete_Sfx;
    private bool One_Time = true;
    // Start is called before the first frame update
    void Start()
    {
        Hitbox = gameObject.GetComponent<Rigidbody2D>();
        Complete_Sfx.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            Timeleft -= Time.deltaTime;
            if (Timeleft <= 0)
            {
                Hitbox.simulated = true;
                timer = false;
            }
        }

        if (Complete)
        {
            timer_Load -= Time.unscaledDeltaTime;
            if (timer_Load <= 0)
            {
                if (Lives_System.Stages > 50)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Lives_System.Stages++;
                    SceneManager.LoadScene(2);
                }
                Complete = false;
            }
        }

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            if (!Door.Enemies_Defeated)
            {
                Debug.Log("Spawn Enemies!");
            }
               
        }

        if (collision.CompareTag("Player"))
        {
            if (Door.Enemies_Defeated)
            {
                if (One_Time)
                {
                    collision.GetComponent<PlayerMovement>().enabled = false;
                    Time.timeScale = 0;
                    Pausing.CantPause = true;
                    Complete = true;
                    Debug.Log("Stage Complete!");
                    Complete_Sfx.Play();
                    One_Time = false;
                }

            }
        }
    }

}
