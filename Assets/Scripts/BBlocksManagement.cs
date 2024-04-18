using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBlocksManagement : MonoBehaviour
{
    public int Door_Statu;
    // List<GetHit_Detection> Break_Blocks = new List<GetHit_Detection>();
    public GameObject[] Break_Blocks_Obj;
    //public GetHit_Detection[] Break_Blocks;
    // Start is called before the first frame update
    void Awake()
    {
        Break_Blocks_Obj = GameObject.FindGameObjectsWithTag("Breakable");

        if (GetHit_Detection.Exit == 1)
        {
            Door_Check();
        }
        Item_Check();
    }

    void Update()
    {
        Door_Statu = GetHit_Detection.Exit;
    }

    public void Door_Check()
    {
        int Random_Num = Random.Range(0, Break_Blocks_Obj.Length);
        Break_Blocks_Obj[Random_Num].GetComponent<GetHit_Detection>().random_number = 25;
        Break_Blocks_Obj[Random_Num].name = "Breakable Block: Exit Door";
        GetHit_Detection.Exit = 0;
    }

    public void Item_Check()
    {
        bool Is_Item_There = false;
        for (int i = 0; i < Break_Blocks_Obj.Length; i++)
        {
            if (Break_Blocks_Obj[i].GetComponent<GetHit_Detection>().random_number == 50)
            {
                Is_Item_There = true;
                break;
            }
        }
        if (!Is_Item_There)
        {
            int Random_Num = Random.Range(0, Break_Blocks_Obj.Length);
            Break_Blocks_Obj[Random_Num].GetComponent<GetHit_Detection>().random_number = 50;
            Break_Blocks_Obj[Random_Num].name = "Breakable Block: Item";
        }
    }
}
