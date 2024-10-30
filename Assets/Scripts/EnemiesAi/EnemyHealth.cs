using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image fillBar;
    public Animator animator;
    public delegate void EnemyKilledHandler(EnemyHealth enemy);
    public event EnemyKilledHandler OnEnemyKilled; // Sự kiện khi quái vật bị tiêu diệt
    DemonSlayer demonSlayer;
    int mapLevel;
    public int bonusHealth = 15;
    public GameObject dmgTextPf;
    public AudioSource audioSource;
    public float pushForce = 10f;
    public float maxPushDistance = 3f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mapLevel = PlayerPrefs.GetInt("SceneIndex");
        maxHealth += mapLevel * bonusHealth;
        demonSlayer = FindObjectOfType<DemonSlayer>();
        if (demonSlayer != null)
            demonSlayer.RegisterEnemy(this);
    }


    private void OnEnable()
    {
        currentHealth = maxHealth;
        UpdateHealth(currentHealth);
    }
    private void UpdateHealth(int currentValue)
    {
        fillBar.fillAmount = (float)currentValue / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        ShowDamage(damage, transform.position);
        currentHealth -= damage;
        audioSource.Play();
        EnemyAi enemyAi = GetComponent<EnemyAi>();
        enemyAi.canMove = false;
        PushForce();
        enemyAi.canMove = true;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyAi.canMove = false;
            animator.SetBool("IsDead", true);
        }
        Debug.Log("Enemy health: " + currentHealth);
        animator.SetTrigger("Hurt");
        UpdateHealth(currentHealth);
        
    }

    private void PushForce()
    {
        Rigidbody2D targetRb = GetComponent<Rigidbody2D>();
        Player player = FindObjectOfType<Player>();

        if (player != null && targetRb != null)
        {
            Vector2 pushDirection = (transform.position - player.transform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position + pushDirection * maxPushDistance;

            StartCoroutine(ApplyPushForce(targetRb, pushDirection, targetPosition));
        }
    }

    private IEnumerator ApplyPushForce(Rigidbody2D targetRb, Vector2 direction, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float pushDuration = 0.5f; 

        while (elapsedTime < pushDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pushDuration;
            Vector2 newPosition = Vector2.Lerp(targetRb.position, targetPosition, t);
            targetRb.MovePosition(newPosition);

            yield return null;
        }
        targetRb.MovePosition(targetPosition);
    }


    private void ShowDamage(int damage, Vector3 position)
    {
        Vector3 vector3 = position + new Vector3(0, 1, -1);
        Debug.Log("Show damage at: " + vector3);
        DamageText damageText = Instantiate(dmgTextPf, vector3, Quaternion.identity).GetComponent<DamageText>();
        Debug.Log("Damage text: " + damageText.transform.position);
        damageText.Initialize(damage);
    }

    public void Die()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}

