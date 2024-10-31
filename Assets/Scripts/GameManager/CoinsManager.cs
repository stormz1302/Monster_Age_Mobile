using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    private const string COIN_KEY = "PlayerCurrency";


    public void Start()
    {
        int coins = LoadCoins();
        if (coins == 0)
            SaveCoins(100000);
    }

    
    public void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save(); 
    }

   
    public int LoadCoins()
    {
        return PlayerPrefs.GetInt(COIN_KEY, 0); 
    }

    public void AddCoins(int coins)
    {
        int currentCoins = LoadCoins();
        currentCoins += coins;
        SaveCoins(currentCoins);
    }

    public void RemoveCoins(int coins)
    {
        int currentCoins = LoadCoins();
        currentCoins -= coins;
        SaveCoins(currentCoins);
    }

   
}
