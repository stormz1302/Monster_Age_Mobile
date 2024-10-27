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

    private void Start()
    {
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetBool("IsDead", true);
        }
        Debug.Log("Enemy health: " + currentHealth);
        animator.SetTrigger("Hurt");
        UpdateHealth(currentHealth);
    }

    public void Die()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}

