using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BodyCount : MonoBehaviour
{
    public float levelTime = 60f; 
    private float currentTime;    
    public TMP_Text timerText;
    
    

    private void Start()
    {
        currentTime = levelTime; 
        UpdateTimerUI();         
        StartCoroutine(TimerCountdown()); 
    }

    // Coroutine để đếm ngược thời gian
    private IEnumerator TimerCountdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); 
            currentTime--;                       
            UpdateTimerUI();                     
        }

        HelicopterSpawner helicopterSpawner = FindObjectOfType<HelicopterSpawner>();
        if (helicopterSpawner != null)
        {
            Debug.Log("Spawning Helicopter 1");
            helicopterSpawner.SpawnHelicopter();
        }

    }

   
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60); 

        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
}
