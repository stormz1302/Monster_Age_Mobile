using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public GameObject[] itemPrefabs;
    public float spawnChance = 0.2f;
    private int enemyKillCounter = 0;
    AudioSource audioSource;
    public List<AudioClip> audioClips; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        float totalWeight = 0;
        foreach (var itemPrefab in itemPrefabs)
        {
            Item item = itemPrefab.GetComponent<Item>();
            if (item != null)
            {
                totalWeight += item.spawnWeight;
            }
        }

        float randomValue = Random.Range(0, totalWeight);

        float cumulativeWeight = 0;
        GameObject selectedPrefab = null;
        foreach (var itemPrefab in itemPrefabs)
        {
            Item item = itemPrefab.GetComponent<Item>();
            if (item != null)
            {
                cumulativeWeight += item.spawnWeight;
                if (randomValue <= cumulativeWeight)
                {
                    selectedPrefab = itemPrefab;
                    break;
                }
            }
        }

        if (selectedPrefab != null)
        {
            Instantiate(selectedPrefab, position, Quaternion.identity);
        }
    }

    public void PlaySound(int audioIndex)
    {
        audioSource.clip = audioClips[audioIndex];
        audioSource.Play();
    }

}
