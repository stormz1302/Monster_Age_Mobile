using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    private const string COIN_KEY = "Coins";
    public TMP_Text coinsText;

    private void Update()
    {
        CoinsDisplay();
    }
    // Lưu trữ coins vào PlayerPrefs
    public void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save(); // Ghi dữ liệu xuống bộ nhớ
    }

    // Lấy số lượng coins từ PlayerPrefs
    public int LoadCoins()
    {
        return PlayerPrefs.GetInt(COIN_KEY, 0); // Trả về 0 nếu chưa có dữ liệu
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

    private void CoinsDisplay()
    {
        coinsText.text = LoadCoins().ToString();
    }
}
