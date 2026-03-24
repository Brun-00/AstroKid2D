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
        Reset();
        StartCoroutine(SpawnCoins());
        StartCoroutine(SpawnGreenCoins());
    }   


    private void Reset()
    {
        coins.value = 0;



    }

    public void AddCoins(int amount)
    {         
        coins.value += amount;
    }

    IEnumerator SpawnCoins()
    {
               while (true)
        {
            SpawnCoin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

   

    void SpawnCoin()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject coin = Instantiate(coinPfb, spawnPosition, Quaternion.identity);

        Destroy(coin, 8f); 
    }

    IEnumerator SpawnGreenCoins()
    {
        while (true)
        {
            SpawnGCoin();
            yield return new WaitForSeconds(spawnGcoinInterval);
        }
    }

    void SpawnGCoin()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        GameObject coin = Instantiate(gcoinPfb, spawnPosition, Quaternion.identity);

        Destroy(coin, 15f);
    }


}
