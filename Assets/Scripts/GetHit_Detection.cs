using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit_Detection : MonoBehaviour
{
    public ExplosionDamage GotHit;
    public GameObject[] PowerUp_Spawn;
    private int random_number;

    void Start()
    {
        random_number = Random.Range(0, PowerUp_Spawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        // Once the explosion collision is detected, the block will be destroyed and has a chance to leave behind an item (W.I.P)
        if (GotHit.Hit)
        {
            /*if (Random.Range(0,10) == 5)
            {

            }
            random_number = Random.Range(0, PowerUp_Spawn.Length);*/
            Destroy(gameObject);
        }
    }
}
