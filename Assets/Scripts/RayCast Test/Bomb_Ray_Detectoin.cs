using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Ray_Detectoin : MonoBehaviour
{
    public bool Face_Up;
    public float Range = 5f;
    public bool Hit;
    public bool Hit2;
    public LayerMask Detection;
    public float The_distance;
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.up;
        Vector2 end;
        if (Face_Up)
        {
             end = transform.position + Vector3.up * Range;
        }
        else
        {
             end = transform.position + Vector3.right * Range;
        }
      
        RaycastHit2D hit = Physics2D.Linecast(transform.position, end, Detection);
       
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Unbreakable"))
            {
                Hit = true;
            }

            if (hit.collider.gameObject.CompareTag("Breakable") || hit.collider.gameObject.CompareTag("Wall2"))
            {
                Hit2 = true;
            }
            The_distance = hit.distance;
            Debug.DrawLine(transform.position, hit.point, Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, end, Color.red);
            Hit = false;
            Hit2 = false;
            The_distance = 0;
        }
    }
}
