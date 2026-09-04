using Assets.Scripts;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform playerSpawnPoint;

    [Header("Animation")]
    public float duration = .2f;
    public Ease ease = Ease.OutBack;

    private GameObject _currentPlayer;

    private void Start()
    {
        // Initialize the game by spawning the player.
        Init();
    }

    public void Init()
    {
        // Start the player setup.
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        // Create the player at the assigned spawn point.
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = playerSpawnPoint.transform.position;

        // Animate the player into the scene by scaling it up.
        _currentPlayer.transform
            .DOScale(0, duration)
            .SetEase(ease)
            .From();
    }
}