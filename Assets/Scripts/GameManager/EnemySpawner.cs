using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Mảng chứa 6 loại quái vật khác nhau
    public Transform player; // Vị trí của player
    public float spawnRadius = 30f; // Bán kính spawn xung quanh player
    public float minDistanceFromPlayer = 28f; // Khoảng cách tối thiểu không spawn quá gần player
    public BoxCollider2D spawnArea; // Collider để xác định khu vực spawn
    public float initialSpawnInterval = 2f;
    public int enemiesPerWave = 3;
    public float waveInterval = 5f;
    

    public int maxEnemies = 10;
    private int currentEnemyCount = 0;

    private float spawnInterval;

    private void Start()
    {
        spawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnEnemies());
        InvokeRepeating("IncreaseMaxEnemies", 10f, 4f);
        
    }

    private void IncreaseMaxEnemies()
    {
        maxEnemies += 1;
    }

    private void Update()
    {
        maxEnemies = Mathf.Min(maxEnemies, 30);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Spawn quái trong mỗi đợt
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (currentEnemyCount < maxEnemies)
                {
                    SpawnEnemy();
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            // Tăng số lượng quái vật trong đợt tiếp theo và giảm thời gian giữa các đợt
            enemiesPerWave += 1;
            spawnInterval *= 0.9f;
            yield return new WaitForSeconds(waveInterval); // Thời gian giữa các đợt
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition;
        bool isInsideCollider;

        do
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minDistanceFromPlayer, spawnRadius);
            spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            // Kiểm tra xem vị trí spawn có nằm trong collider không
            isInsideCollider = spawnArea.OverlapPoint(spawnPosition);
        }
        while (!isInsideCollider || Vector2.Distance(spawnPosition, player.position) < minDistanceFromPlayer);

        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);

        currentEnemyCount++;

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyKilled += OnEnemyKilled;
        }
        
    }

    private void OnEnemyKilled(EnemyHealth enemyHealth)
    {
        enemyHealth.OnEnemyKilled -= OnEnemyKilled;
        Debug.Log("Enemy killed 1");
        currentEnemyCount--;
        ItemManager.Instance.OnEnemyKilled(enemyHealth.transform);
        
    }
        
}
