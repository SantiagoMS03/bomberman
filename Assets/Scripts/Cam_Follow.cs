using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    [Space(2f)]
    [Header("Camera Distance")]
    public float X_Start_Position;
    public float X_End_Position;
    public float Y_Start_Position;
    public float Y_End_Position;
    public float Z_Start_Position;
    public float Z_End_Position;
    [Space(2f)]
    [Header("Following Player Setup")]
    public Vector3 newPos;
    public Vector3 newPos2;
    public Vector3 velocity = Vector3.one;

    public void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
    }


    void Update()
    {
        newPos = new Vector3(target.position.x + offset.x, Y_Start_Position, target.position.z + offset.z);
        newPos2 = new Vector3(target.position.x + offset.x, Y_Start_Position, target.position.z + 0);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, 0);
        Vector3 clampedPosition = transform.position;
        Vector3 clampedPosition2 = transform.position;
        Vector3 clampedPosition3 = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, X_Start_Position, X_End_Position);
        clampedPosition2.y = Mathf.Clamp(clampedPosition2.y, Y_Start_Position, Y_End_Position);
        clampedPosition3.z = Mathf.Clamp(clampedPosition3.z, Z_Start_Position, Z_End_Position);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, X_Start_Position, X_End_Position), Y_Start_Position, transform.position.z);
    }
}
