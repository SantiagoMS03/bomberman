using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_Intro_Text : MonoBehaviour
{
    public Lives_System Curr_Stage;
    public TextMeshProUGUI Stage_num;
    public TextMeshProUGUI Stage_num_Shadow;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject find = GameObject.FindWithTag("Stage n Life Sys");
       // Curr_Stage = find.GetComponent<Lives_System>();
        Stage_num.text = Lives_System.Stages.ToString();
        Stage_num_Shadow.text = Stage_num.text;
    }
}
