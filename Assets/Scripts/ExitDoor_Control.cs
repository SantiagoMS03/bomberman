using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor_Control : MonoBehaviour
{
    public static bool DoorSwpawned;
    public bool Checking;
    public bool Door_Spawn;
    public bool Enemies_Defeated;
    public AudioSource Enemies_Clear_Sfx;
    public GameObject[] Enemies;
    public BoxCollider2D Hitbox;
    //public List<GameObject> The_nemies = new List<GameObject>();
    private int Once;
    // Start is called before the first frame update
    void Awake()
    {
        Door_Spawn = true;
        DoorSwpawned = true;
        Hitbox = gameObject.GetComponent<BoxCollider2D>();
      //  GameObject[] Set = GameObject.FindGameObjectsWithTag("Enemies");
      //  The_nemies = new List<GameObject>(Set);
        Check_Enemies();
    }

    // Update is called once per frame
    void Update()
    {

        /* if (The_nemies == null || The_nemies.Count <= 0)
         {
             Hitbox.isTrigger = true;
             Enemies_Defeated = true;
         }
         */

        if (Checking)
        {
            if (Enemies == null || Enemies.Length == 0)
            {
                Hitbox.isTrigger = true;
                Enemies_Defeated = true;
                Checking = false;
                All_Clear_Sfx();
            }
        }
    }

    public void Check_Enemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemies");
        if (Enemies == null)
        {
            Hitbox.isTrigger = true;
            Enemies_Defeated = true;
            Checking = false;
            All_Clear_Sfx();
        }
    }

    public void All_Clear_Sfx()
    {
        if (Once == 0)
        {
            Enemies_Clear_Sfx.Play();
            Once++;
        }
    }

}
