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
        // Initialize health and find the flash effect if needed.
        Init();
        _currentLife = startLife;

        if (_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        // Reset the health state.
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int damage)
    {
        // Ignore damage after the object has died.
        if (_isDead) return;

        _currentLife -= damage;

        // Play the damage sound with a random pitch.
        if (damageSound != null)
        {
            damageSound.pitch =
                UnityEngine.Random.Range(0.6f, 1.4f);

            damageSound.Play();
        }

        // Clamp health and trigger death when it reaches zero.
        if (_currentLife <= 0)
        {
            _currentLife = 0;
            Die();
        }

        // Flash the object when it takes damage.
        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Die()
    {
        // Mark the object as dead.
        _isDead = true;

        // Destroy the object after the configured delay.
        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }

        // Notify any listeners that the object has died.
        OnKill?.Invoke();
    }
}