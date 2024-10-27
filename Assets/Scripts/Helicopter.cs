using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private Vector2 targetPosition;  // Vị trí mục tiêu
    private float speed;             // Tốc độ di chuyển
    public int maxHealth = 100;
    private int currentHealth;
    private LoneSurvivor loneSurvivor;
    [SerializeField] private Animator animator;
    public bool isComplete = false;
    public AudioSource audioSource;

    public float invulnerabilityDuration = 2f; // Thời gian bất tử sau khi bị tấn công
    private bool isInvulnerable = false; // Trạng thái bất tử

    private void Start()
    {
        currentHealth = maxHealth;
        loneSurvivor = FindObjectOfType<LoneSurvivor>();
        JoystickPlayerExample joystick = FindObjectOfType<JoystickPlayerExample>();
        joystick.helicopterTransform = transform;
        UpdateHelicopterHealth();
    }

    public void Initialize(Vector2 targetPos, float moveSpeed)
    {
        targetPosition = targetPos;
        targetPosition.y = 10; // Đảm bảo Y = 0
        speed = moveSpeed;
        isComplete = true;
        animator.SetBool("Fly", true);
    }

    private void Update()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float step = speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x, transform.position.y) + direction * step;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isComplete)
            {
                Debug.Log("Helicopter hit Player");
                EndScreen endScreen = FindObjectOfType<EndScreen>();
                if (endScreen != null)
                {
                    endScreen.NotiMissionState(true);
                }
            }
        }
    }

    private void UpdateHelicopterHealth()
    {
        if (loneSurvivor != null && loneSurvivor.loneSurvivor)
        {
            loneSurvivor.UpdateHelicopterHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; 

        if (loneSurvivor != null && loneSurvivor.loneSurvivor)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                EndScreen endScreen = FindObjectOfType<EndScreen>();
                if (endScreen != null)
                {
                    endScreen.NotiMissionState(false);
                }
                Destroy(gameObject);
            }

            UpdateHelicopterHealth();
            StartCoroutine(InvulnerabilityTimer()); 
        }
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true; 
        yield return new WaitForSeconds(invulnerabilityDuration); 
        isInvulnerable = false; 
    }
}
