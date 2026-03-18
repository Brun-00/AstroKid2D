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

    private void Awake()
    {
        if (health != null)
        {
            health.OnKill += OnEnemyKill;
            
        }
    }

    private void OnEnemyKill()
    {
        health.OnKill -= OnEnemyKill;
        PlayDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealhtBase>();

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

    
}
