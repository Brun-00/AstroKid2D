using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 velocity;

    public float moveSpeed;

   
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            //rb.MovePosition(rb.position - moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //rb.MovePosition(rb.position + moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
}
