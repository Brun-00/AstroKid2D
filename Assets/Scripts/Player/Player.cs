using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public Vector2 friction = new Vector2(.1f,0);
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce = 2;

    [Header("Animation")]
    public float jumpScaley = 2f;
    public float jumpScalex = 0f;
    public float duration = .2f;
    public Ease ease = Ease.OutBack;
    public float runScalex = 0.8f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private bool _isGrounded;
    private bool _wasGrounded;

    [Header("Land Animation")]
    public float landScaleX = 1.3f;
    public float landScaleY = 0.7f;
    public float landDuration = 0.15f;


    private float _currentMoveSpeed;


    void Update()
    {
        CheckGround();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentMoveSpeed = runSpeed;

            HandleScaleRun();
            
        }
        else
        {
            rb.transform.localScale = Vector2.one;
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
    private void CheckGround()
    {
        _wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (!_wasGrounded && _isGrounded)
        {
            HandleLandScale();
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            rb.transform.localScale = Vector2.one;

            DOTween.Kill(rb.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        rb.transform.DOScaleY(jumpScaley, duration).SetLoops(2,LoopType.Yoyo).SetEase(ease);
        rb.transform.DOScaleX(jumpScalex, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);

    }

    private void HandleScaleRun()
    {
        rb.transform.DOScaleX(runScalex, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void HandleLandScale()
    {
        DOTween.Kill(rb.transform);

        rb.transform.DOScale(
            new Vector3(landScaleX, landScaleY, 1),
            landDuration
        )
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.OutQuad);
    }
}
