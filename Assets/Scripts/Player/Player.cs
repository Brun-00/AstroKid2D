using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public HealhtBase _healthBase;

    [Header("Setup")]
    public SOPlayerSetup playerSetup;

    public Ease ease = Ease.OutBack;
    public float runScalex = 0.8f;
    public Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private float _currentMoveSpeed;
    private bool _isGrounded;
    private bool _isDead = false;

    public GunBase gun;

    public ParticleSystem particlePrefab;


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
        animator.SetTrigger(playerSetup.triggerDeath);
        _isDead = true;

        if (gun != null)
        {
            gun.StopShooting();
            gun.enabled = false; 
        }

    }
    private void Update()
    {
        
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        HandleJump();
        HandleMovement();

        animator.SetBool("IsJumping", !_isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void HandleMovement()
    {

        if (_isDead) return;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentMoveSpeed = playerSetup.runSpeed;
            animator.speed = 1.3f;
        }
        else
        {
            animator.speed = 1f;
            _currentMoveSpeed = playerSetup.moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-_currentMoveSpeed, rb.velocity.y);
            if(rb.transform.localScale.x != -1)
            {
                    rb.transform.DOScaleX(-1, playerSetup.swipeDuration).SetEase(ease);
            }
            animator.SetBool(playerSetup.boolRun, true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(_currentMoveSpeed, rb.velocity.y);
            if (rb.transform.localScale.x != 1)
            {
                rb.transform.DOScaleX(1, playerSetup.swipeDuration).SetEase(ease);
            }
            animator.SetBool(playerSetup.boolRun, true);
        }
        else
        {
            animator.SetBool(playerSetup.boolRun, false);
        }

        if (rb.velocity.x > 0)
        {
            rb.velocity += playerSetup.friction;
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity -= playerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (_isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            ParticleSystem ps = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ps.Play();
            rb.velocity = Vector2.up * playerSetup.jumpForce;
            Destroy(ps.gameObject, 5);

        }
    }

    public bool IsDead()
    {
        return _isDead;
    }



}