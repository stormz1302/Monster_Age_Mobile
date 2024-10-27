using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public GameObject helicopterPrefab;  // Prefab trực thăng
    public Transform playerTransform;    // Transform của Player
    public float spawnHeight = 50f;      // Khoảng cách spawn trực thăng phía trên Player
    public float moveSpeed = 3f;         // Tốc độ di chuyển của trực thăng

    // Hàm gọi để spawn trực thăng phía trên Player và bắt đầu di chuyển
    public void SpawnHelicopter()
    {
        if (helicopterPrefab != null && playerTransform != null)
        {
            Debug.Log("Spawning Helicopter 2");
            // Tính vị trí spawn (phía trên Player)
            Vector2 spawnPosition = playerTransform.position + new Vector3(0, spawnHeight, 0);
            Vector2 targetPosition = playerTransform.position; // Vị trí Player tại thời điểm spawn

            // Tạo trực thăng từ prefab
            GameObject helicopter = Instantiate(helicopterPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Helicopter: " + helicopter.transform.position);
            

            // Lấy script Helicopter từ prefab và khởi tạo
            Helicopter helicopterScript = helicopter.GetComponent<Helicopter>();
            helicopterScript.Initialize(targetPosition, moveSpeed); 
        }
        
    }
}
