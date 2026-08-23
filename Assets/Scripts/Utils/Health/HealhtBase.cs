using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealhtBase : MonoBehaviour
{
    public Action OnKill;
    public float startLife = 10;
    public bool destroyOnKill = false;
    public float delayToKill = 0.8f;
    public AudioSource damageSound;

    [SerializeField] private FlashColor _flashColor;

    private bool _isDead = false;
    private float _currentLife;

    public float CurrentLife => _currentLife;
    public float MaxLife => startLife;

    private void Awake()
    {
        Init();
        _currentLife = startLife;

        if (_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentLife -= damage;

        if (damageSound != null)
        {
            damageSound.pitch = UnityEngine.Random.Range(0.6f, 1.4f);
            damageSound.Play();
        }

        if (_currentLife <= 0)
        {
            _currentLife = 0;
            Die();
        }

        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Die()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }

        OnKill?.Invoke();
    }
}