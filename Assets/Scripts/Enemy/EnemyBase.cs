using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    public Animator animator;
    public string attackTrigger = "Attack";
    public HealhtBase health;
    public string deathTrigger = "Death";

    public Rigidbody2D rb;
    public Transform player;
    public float moveSpeed = 2f;

    public BoxCollider2D boxCollider;
    public BoxCollider2D hitbox;

    public AudioSource deathSound;

    private void Awake()
    {
        // Subscribe to the health death event.
        if (health != null)
        {
            health.OnKill += OnEnemyKill;
        }
    }

    private void Start()
    {
        // Find the player using its tag.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // Continuously move toward the player.
        MoveToPlayer();
    }

    private void OnEnemyKill()
    {
        // Disable player tracking and collision after death.
        player = null;
        boxCollider.enabled = false;
        hitbox.enabled = false;

        health.OnKill -= OnEnemyKill;

        // Play the death sound with a slight pitch variation.
        deathSound.pitch = Random.Range(0.6f, 1.4f);
        deathSound.Play();

        PlayDeathAnimation();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<HealhtBase>();

        // Damage any object with a health component on contact.
        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        // Trigger the enemy attack animation.
        animator.SetTrigger(attackTrigger);
    }

    private void PlayDeathAnimation()
    {
        // Trigger the enemy death animation.
        animator.SetTrigger(deathTrigger);
    }

    public void Damage(int amount)
    {
        // Forward received damage to the health component.
        health.Damage(amount);
    }

    void MoveToPlayer()
    {
        // Stop moving when there is no player target.
        if (player == null) return;

        // Move directly toward the player's current position.
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        Flip(direction);
    }

    void Flip(Vector2 direction)
    {
        // Do not flip when there is no horizontal movement.
        if (direction.x == 0) return;

        Vector3 scale = transform.localScale;

        // Flip the sprite based on the movement direction.
        scale.x = direction.x > 0
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}