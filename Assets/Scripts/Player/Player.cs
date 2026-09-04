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

    public GameObject deathUI;

    public AudioSource jumpSound;

    private void Awake()
    {
        // Subscribe to the health death event.
        if (_healthBase != null)
        {
            _healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        // Stop listening for further death events and handle the death sequence.
        _healthBase.OnKill -= OnPlayerKill;
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        // Play the death animation and disable player controls.
        animator.SetTrigger(playerSetup.triggerDeath);
        _isDead = true;

        if (gun != null)
        {
            gun.StopShooting();
            gun.enabled = false;
        }

        deathUI.SetActive(true);

        // Give the death animation time to play before pausing.
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Check whether the player is currently touching the ground.
        _isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        HandleJump();
        HandleMovement();

        // Update animation parameters based on the player's movement.
        animator.SetBool("IsJumping", !_isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void HandleMovement()
    {
        // Prevent movement after death.
        if (_isDead) return;

        // Switch between normal and running speed.
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
            // Move left while keeping the vertical velocity.
            rb.velocity = new Vector2(
                -_currentMoveSpeed,
                rb.velocity.y
            );

            // Flip the player toward the movement direction.
            if (rb.transform.localScale.x != -1)
            {
                rb.transform
                    .DOScaleX(-1, playerSetup.swipeDuration)
                    .SetEase(ease);
            }

            animator.SetBool(playerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Move right while keeping the vertical velocity.
            rb.velocity = new Vector2(
                _currentMoveSpeed,
                rb.velocity.y
            );

            // Flip the player toward the movement direction.
            if (rb.transform.localScale.x != 1)
            {
                rb.transform
                    .DOScaleX(1, playerSetup.swipeDuration)
                    .SetEase(ease);
            }

            animator.SetBool(playerSetup.boolRun, true);
        }
        else
        {
            // Stop the running animation when there is no input.
            animator.SetBool(playerSetup.boolRun, false);
        }

        // Apply horizontal friction to the player's movement.
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
        // Prevent jumping after death.
        if (_isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            // Spawn the jump effect at the player's position.
            ParticleSystem ps = Instantiate(
                particlePrefab,
                transform.position,
                Quaternion.identity
            );

            ps.Play();

            // Apply the jump force.
            rb.velocity =
                Vector2.up * playerSetup.jumpForce;

            // Remove the particle effect after a few seconds.
            Destroy(ps.gameObject, 5);

            // Play the jump sound with a random pitch.
            jumpSound.pitch =
                Random.Range(0.6f, 1.4f);

            jumpSound.Play();
        }
    }

    public bool IsDead()
    {
        // Return the player's current death state.
        return _isDead;
    }
}