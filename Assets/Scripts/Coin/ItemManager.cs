using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ItemManager : Singleton<ItemManager>
{
    public GameObject coinPfb;
    public GameObject gcoinPfb;

    public float spawnInterval = 4f;
    public float spawnGcoinInterval = 10f;

    public float minY;
    public float maxY;

    public float minX;
    public float maxX;

    public SOInt coins;

    private void Start()
    {
        // Reset the player's coin count and start both spawn loops.
        Reset();
        StartCoroutine(SpawnCoins());
        StartCoroutine(SpawnGreenCoins());
    }

    private void Reset()
    {
        // Reset the total number of collected coins.
        coins.value = 0;
    }

    public void AddCoins(int amount)
    {
        // Add the collected amount to the total.
        coins.value += amount;
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            // Spawn a regular coin and wait before the next one.
            SpawnCoin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        // Pick a random position inside the spawn area.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject coin = Instantiate(
            coinPfb,
            spawnPosition,
            Quaternion.identity
        );

        // Remove the coin if it is not collected in time.
        Destroy(coin, 8f);
    }

    IEnumerator SpawnGreenCoins()
    {
        while (true)
        {
            // Spawn a green coin and wait before the next one.
            SpawnGCoin();
            yield return new WaitForSeconds(spawnGcoinInterval);
        }
    }

    void SpawnGCoin()
    {
        // Pick a random position inside the spawn area.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject coin = Instantiate(
            gcoinPfb,
            spawnPosition,
            Quaternion.identity
        );

        // Remove the green coin if it is not collected in time.
        Destroy(coin, 15f);
    }
}