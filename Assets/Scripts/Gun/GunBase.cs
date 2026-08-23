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

    public AudioSource shootSound;



    float nextShotTime = 0f;

    void Update()
    {
        if (player != null && player.IsDead()) return;

        if (Input.GetMouseButton(0) && Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

 

    public void Shoot()
    {
        if (player != null && player.IsDead()) return;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootingPosition.position;

        shootSound.pitch = Random.Range(0.6f, 1.4f);
        shootSound.Play();

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
