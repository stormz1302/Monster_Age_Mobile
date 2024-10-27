using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float SafeTime = 1f;
    private float safeTimeCooldown;
    private int currentHealth;
    public Animator animator;
    public Healthbar healthbar;
    private EndScreen endScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar = healthbar.GetComponent<Healthbar>();
        healthbar.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        safeTimeCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (safeTimeCooldown <= 0)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //animator.SetBool("isDead", true);
                //Die();
                StartCoroutine(WaitForAnimation());
            }
            healthbar.UpdateHealth(currentHealth, maxHealth);
            safeTimeCooldown = SafeTime;
        }
        
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbar.UpdateHealth(currentHealth, maxHealth);
    }

    private IEnumerator WaitForAnimation()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        Die();
    }
    private void Die()
    {
        Time.timeScale = 0f;
        endScreen = FindObjectOfType<EndScreen>();
        endScreen.NotiMissionState(false);
    }
}
