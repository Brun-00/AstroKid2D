using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtBase : MonoBehaviour
{
    public float startLife = 10;

    public bool destroyOnKill = false;

    private float _currentLife;

    [SerializeField] private FlashColor _flashColor; 

    private bool _isDead = false;

    private void Awake()
    {
        _currentLife = startLife;
        if(_flashColor == null)
        {
            _flashColor = GetComponent<FlashColor>();
        }
    }

    private void Init()
    {
        _isDead = false;

    }

    public void Damage(int damage)
    {
        _currentLife -= damage;
        if(_currentLife <= 0)
        {
            Die();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Die()
    {
        _isDead = true;
        if(destroyOnKill)
        {
            Destroy(gameObject);
        }
    }
}
