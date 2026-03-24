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


    private void Awake()
    {
        if (health != null)
        {
            health.OnKill += OnEnemyKill;
            
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }



    private void OnEnemyKill()
    {
        player = null;
        boxCollider.enabled = false;
        hitbox.enabled = false;
        health.OnKill -= OnEnemyKill;
        PlayDeathAnimation();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        var health = collision.GetComponent<HealhtBase>();

        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }


    private void PlayAttackAnimation()
    {
        animator.SetTrigger(attackTrigger);
    }

    private void PlayDeathAnimation()
    {
        animator.SetTrigger(deathTrigger);
    }

    public void Damage(int amount)
    {
        health.Damage(amount);
    }

    void MoveToPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        Flip(direction);
    }

    void Flip(Vector2 direction)
    {
        if (direction.x == 0) return;

        Vector3 scale = transform.localScale;

        scale.x = direction.x > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }


}
