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

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootingPosition.position;
        projectile.side = playerSide.transform.localScale.x;
    }
}
