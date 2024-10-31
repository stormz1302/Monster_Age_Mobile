using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text coinsTextShop;


    private void Update()
    {
        DisplayCoins();
    }
    private void DisplayCoins()
    {
        CoinsManager coinsManager = FindObjectOfType<CoinsManager>();
        int coins = coinsManager.LoadCoins();
        coinsText.text = coins.ToString();
        coinsTextShop.text = coins.ToString();
    }
}
