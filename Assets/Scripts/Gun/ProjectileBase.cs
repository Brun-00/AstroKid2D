using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 10f;
    public float destroyTime = 2f;
    public int damage = 1;

    private Vector2 _direction;
    private Rigidbody2D _rb;

    private void Awake()
    {
        // Get the projectile Rigidbody and set its lifetime.
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(Vector2 dir)
    {
        // Normalize the direction so speed stays consistent.
        _direction = dir.normalized;

        // Apply movement using physics.
        _rb.velocity = _direction * speed;

        // Rotate the projectile to face its movement direction.
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();

        // Damage the enemy if the projectile hits one.
        if (enemy != null)
        {
            enemy.Damage(damage);
        }

        // Destroy the projectile after any collision.
        Destroy(gameObject);
    }
}