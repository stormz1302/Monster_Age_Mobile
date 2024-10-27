using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemonSlayer : MonoBehaviour
{
    
    private int enemyKilledCount = 0; 
    public TMP_Text enemyCountText; 
    public int totalEnemyCount = 50;
    private HelicopterSpawner helicopterSpawner;
    private void Start()
    {
        helicopterSpawner = FindObjectOfType<HelicopterSpawner>();

        UpdateEnemyCountText();
    }

    public void RegisterEnemy(EnemyHealth enemyHealth)
    {
        enemyHealth.OnEnemyKilled += CountEnemyKilled;
    }

    private void CountEnemyKilled(EnemyHealth enemyHealth)
    {
        enemyHealth.OnEnemyKilled -= CountEnemyKilled;
        Debug.Log("Enemy killed");
        enemyKilledCount++; 
        UpdateEnemyCountText(); 
    }

    private void UpdateEnemyCountText()
    {
        // Cập nhật nội dung của UI Text
        enemyCountText.text = enemyKilledCount.ToString() + "/" + totalEnemyCount.ToString();
        if (enemyKilledCount == totalEnemyCount)
        {
            helicopterSpawner.SpawnHelicopter();
            Debug.Log("Mission Completed");
        }
    }
}
