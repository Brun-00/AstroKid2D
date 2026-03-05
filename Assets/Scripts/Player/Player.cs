using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 friction = new Vector2(.1f,0);

    public float moveSpeed;
    public float runSpeed;

    private float _currentMoveSpeed;

    public float jumpForce = 2;


    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentMoveSpeed = runSpeed;
        }
        else
        {
            _currentMoveSpeed = moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            
            rb.velocity = new Vector2(-_currentMoveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            rb.velocity = new Vector2(_currentMoveSpeed, rb.velocity.y);
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
