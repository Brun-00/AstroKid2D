using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ItemManager : Singleton<ItemManager>
{
    public TMPro.TextMeshProUGUI coinsText;

    public int coins;

    private void Start()
    {
        Reset();
    }   


    private void Reset()
    {
        coins = 0;
        coinsText.text = ("x" + coins.ToString());


    }

    public void AddCoins(int amount = 1)
    {         coins+= amount;
    }

    private void Update()
    {
        coinsText.text = ("x" + coins.ToString());
    }
}
