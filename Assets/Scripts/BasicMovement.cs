using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private float timer;
    private Vector3 moveDirection;
    public float changeDirectionInterval = 0f;
    public bool isMoving = true;
    private LayerMask Detection;
    private char rotation;

    void Start()
    {
        ResetDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<Vector2> list = new List<Vector2>();
        Vector2 position = transform.position;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 point = collision.GetContact(i).point - position;
            if (point.x > 0)
            {
                //if (!list.Contains(Vector2.left)) list.Add(Vector2.left);
                list.Add(Vector2.left);
            }

            if (point.x < 0)
            {
                list.Add(Vector2.right);
                //if (!list.Contains(Vector2.right)) list.Add(Vector2.right);

            }
            if (point.y > 0)
            {
                list.Add(Vector2.down);
                //if (!list.Contains(Vector2.down)) list.Add(Vector2.down);
            }
            if (point.y < 0)
            {
                list.Add(Vector2.up);
                //if (!list.Contains(Vector2.up)) list.Add(Vector2.up);
            }
        }
        //foreach (Vector2 v in list)
        //{
        //    if (v == Vector2.up) print("up");
        //    if (v == Vector2.down) print("down");
        //    if (v == Vector2.left) print("left");
        //    if (v == Vector2.right) print("right");
        //}
        moveDirection = list[Random.Range(0, list.Count)];
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            InvertDirection();
        }

        List<Vector2> list = new List<Vector2>();
        Vector2 position = transform.position;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 point = collision.GetContact(i).point - position;
            if (point.x > 0)
            {
                //if (!list.Contains(Vector2.left)) list.Add(Vector2.left);
                list.Add(Vector2.left);
            }

            if (point.x < 0)
            {
                list.Add(Vector2.right);
                //if (!list.Contains(Vector2.right)) list.Add(Vector2.right);

            }
            if (point.y > 0)
            {
                list.Add(Vector2.down);
                //if (!list.Contains(Vector2.down)) list.Add(Vector2.down);
            }
            if (point.y < 0)
            {
                list.Add(Vector2.up);
                //if (!list.Contains(Vector2.up)) list.Add(Vector2.up);
            }
        }
 /*       foreach (Vector2 v in list)
        {
            if (v == Vector2.up) print("up");
            if (v == Vector2.down) print("down");
            if (v == Vector2.left) print("left");
            if (v == Vector2.right) print("right");
        }*/
        moveDirection = list[Random.Range(0, list.Count)];

    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                ResetDirection();
            }


        }
    }

    bool isStuck() // to do
    {
        return true;
    }

    void ResetDirection()
    {
        ChangeDirection();
        ResetTimer();
    }



    //void findNewDirection()
    //{
    //    Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    //    Vector2 bestDirection = Vector2.up;
    //    foreach(Vector2 direction in directions)
    //    {
    //        RaycastHit2D hit = Physics2D.Linecast(transformation.position, )
            
    //    }
    //}

    public void ChangeDirection()
    {
        float rand = Random.Range(0, 4);
        if (rand < 1)
        {
            moveDirection = Vector2.up;
        }
        else if (rand < 2)
        {
            moveDirection = Vector2.left;
        }
        else if (rand < 3)
        {
            moveDirection = Vector2.down;
        }
        else if (rand < 4)
        {
            moveDirection = Vector2.right;
        }
    }

    void InvertDirection()
    {
        moveDirection *= -1;
        ResetTimer();
    }

    void ResetTimer()
    {
        timer = changeDirectionInterval;
    }

    void RotateClockwise()
    {
        //print("move direction" + moveDirection);
        if (moveDirection == Vector3.up) moveDirection = Vector3.right;
        else if (moveDirection == Vector3.right) moveDirection = Vector3.down;
        else if (moveDirection == Vector3.down) moveDirection = Vector3.left;
        else if (moveDirection == Vector3.left) moveDirection = Vector3.up;
        ResetTimer();
        ;
    }

    void RotateCounterClockwise()
    {
        //print("move direction" + moveDirection);
        if (moveDirection == Vector3.up) moveDirection = Vector3.left;
        else if (moveDirection == Vector3.left) moveDirection = Vector3.down;
        else if (moveDirection == Vector3.down) moveDirection = Vector3.right;
        else if (moveDirection == Vector3.right) moveDirection = Vector3.up;
        ResetTimer();
        ;
    }

}