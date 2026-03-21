using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectilePrefab;
    public Transform shootingPosition;
    public float timeBetweenShots = 0.3f;
    public Transform playerSide;

    private Coroutine _currentCoroutine;

    public Player player;



    private void Update()
    {
        if (player != null && player.IsDead()) return;

        if (Input.GetMouseButtonDown(0))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        
    }

    public void Shoot()
    {
        if (player != null && player.IsDead()) return;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootingPosition.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - shootingPosition.position).normalized;

        projectile.SetDirection(direction);
    }

    public void StopShooting()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
