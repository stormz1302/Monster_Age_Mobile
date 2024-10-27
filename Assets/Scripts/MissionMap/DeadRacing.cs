using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeadRacing : MonoBehaviour
{
    public Transform player;      // Tham chiếu đến Player
    public Transform target;      // Tham chiếu đến mục tiêu đích
    public TMP_Text distanceText; // Text để hiển thị khoảng cách
    private HelicopterSpawner helicopterSpawner;
    bool isCompleted = false;

    void Update()
    {
        if (!isCompleted)
        {
            float distance = Vector2.Distance(player.position, target.position) / 10;
            distanceText.text = $"{distance:F2}m";
            Completed(distance);
        }
       
    }

    private void Start()
    {
        helicopterSpawner = FindObjectOfType<HelicopterSpawner>();
    }

    private void Completed(float distance)
    {
        if (distance <= 0.3f)
        {
            helicopterSpawner = FindObjectOfType<HelicopterSpawner>();
            if (helicopterSpawner != null)
            {
                Debug.Log("Spawning Helicopter 1");
                helicopterSpawner.SpawnHelicopter();
            }
            isCompleted = true;
            distanceText.text = $"0m";
        }
    }
}
