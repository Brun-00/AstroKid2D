using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 friction = new Vector2(.1f,0);

    public float moveSpeed;

    public float jumpForce = 2;


    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //rb.MovePosition(rb.position - moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rb.MovePosition(rb.position + moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if(rb.velocity.x >0)
        {
            rb.velocity += friction;
        }
        else if (rb.velocity.x <0)
        {
            rb.velocity -= friction;
        }

    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
