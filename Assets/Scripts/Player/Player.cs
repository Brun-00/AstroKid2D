using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public HealhtBase _healthBase;

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
    public string triggerDeath = "Death";
    public Animator animator;
    public float swipeDuration = .2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private float _currentMoveSpeed;
    private bool _isGrounded;
    private bool _isDead = false;


    private void Awake()
    {
        if(_healthBase != null)
        {
            _healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        _healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
        _isDead = true;

    }
    private void Update()
    {
        HandleJump();
        HandleMovement();
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


    }

    private void HandleMovement()
    {

        if (_isDead) return;
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

}