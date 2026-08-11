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
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;

        // Move usando física
        _rb.velocity = _direction * speed;

        // Rotaciona na direção do tiro
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }
}