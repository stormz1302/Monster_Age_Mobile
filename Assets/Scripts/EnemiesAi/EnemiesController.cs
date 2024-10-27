using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    Player player;
    private PlayerHealth playerHealth;
    private Helicopter helicopter;
    public Animator animator;

    public int minDamage;
    public int maxDamage;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            InvokeRepeating("DamagePlayer", 0, 0.5f);
        }
        if (collision.CompareTag("Helicopter"))
        {
            helicopter = collision.GetComponent<Helicopter>();
            InvokeRepeating("DamageHelicopter", 0, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        playerHealth.TakeDamage(damage);
        animator.SetTrigger("Attack");
    }

    void DamageHelicopter()
    {
        int damage = Random.Range(2, 5);
        helicopter.TakeDamage(damage);
        animator.SetTrigger("Attack");
    }

}
