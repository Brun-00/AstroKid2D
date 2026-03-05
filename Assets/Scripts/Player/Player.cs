using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce = 2;

    [Header("Animation")]
    public float jumpScaley = 2f;
    public float jumpScalex = 0f;
    public float duration = .2f;
    public Ease ease = Ease.OutBack;
    public float runScalex = 0.8f;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public Animator animator;
    public float swipeDuration = .2f;

    private float _currentMoveSpeed;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {


        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentMoveSpeed = runSpeed;
            animator.speed = 1.3f;
        }
        else
        {
            animator.speed = 1f;
            _currentMoveSpeed = moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-_currentMoveSpeed, rb.velocity.y);
            if(rb.transform.localScale.x != -1)
            {
                    rb.transform.DOScaleX(-1, swipeDuration).SetEase(ease);
            }
            animator.SetBool(boolRun, true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(_currentMoveSpeed, rb.velocity.y);
            if (rb.transform.localScale.x != 1)
            {
                rb.transform.DOScaleX(1, swipeDuration).SetEase(ease);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
        }

        if (rb.velocity.x > 0)
        {
            rb.velocity += friction;
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;

        }
    }

}