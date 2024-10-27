using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance; 
    public GameObject[] itemPrefabs; 
    public float spawnChance = 0.2f; 
    private int enemyKillCounter = 0; 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnEnemyKilled(Transform enemyPosition)
    {
        enemyKillCounter++; 

        
        if (enemyKillCounter >= 9 || Random.value < spawnChance)
        {
            SpawnItem(enemyPosition.position); 
            enemyKillCounter = 0; 
        }
    }

    private void SpawnItem(Vector2 position)
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[randomIndex], position, Quaternion.identity); 
    }

}
