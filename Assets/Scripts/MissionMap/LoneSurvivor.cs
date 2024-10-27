using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoneSurvivor : MonoBehaviour
{
    public bool loneSurvivor;

    [SerializeField] private TMP_Text helicopterHealthText;  // Hiển thị máu máy bay
    [SerializeField] private TMP_Text countdownText;         // Hiển thị thời gian đếm ngược
    [SerializeField] private Animator animator;
    Helicopter helicopter;
    public float countdownTime = 300f; // Thời gian đếm ngược (5 phút = 300 giây)

    private void Start()
    {
        StartCoroutine(StartCountdown());
        helicopter = FindObjectOfType<Helicopter>();
        UpdateHelicopterHealth(helicopter.maxHealth);
    }

    public void UpdateHelicopterHealth(int health)
    {
        helicopterHealthText.text = health + "%";
    }

    IEnumerator StartCountdown()
    {
        float remainingTime = countdownTime;

        while (remainingTime > 0)
        {
            // Cập nhật và hiển thị thời gian còn lại trên giao diện theo "MM:SS"
            countdownText.text = FormatTime(remainingTime);
            Debug.Log(FormatTime(remainingTime));

            remainingTime -= Time.deltaTime;
            yield return null; // Chờ mỗi frame
        }
        helicopter.isComplete = true;
        helicopter.audioSource.Play();
        // Khi hết thời gian
        animator.SetBool("Fly", true);
    }

    // Hàm định dạng thời gian thành "MM:SS"
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);  // Lấy số phút
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);  // Lấy số giây còn lại
        return string.Format("{0:00}:{1:00}", minutes, seconds);  // Định dạng MM:SS
    }
}
