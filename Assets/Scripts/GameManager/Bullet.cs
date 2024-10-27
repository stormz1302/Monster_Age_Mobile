using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private Weapon weapon;

    public bool isPlayerBullet;
    public float pushForce = 5f;
    public float maxPushDistance = 3f;
    public AudioSource audioSource;
    private bool bulletOfSniper;
    private int enemyHitCount = 0;  

    private void Start()
    {
        if (isPlayerBullet)
        {
            weapon = FindObjectOfType<Weapon>();
            damage = weapon.Damage;
            Debug.Log("damage: " + damage);
            bulletOfSniper = weapon.weaponType == "Sniper";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerBullet)
        {
            damage = Random.Range(5, 10);
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy") && isPlayerBullet)
        {
            HandleEnemyHit(collision);
        }

        if (collision.CompareTag("Helicopter") && !isPlayerBullet)
        {
            damage = Random.Range(2, 5);
            collision.GetComponent<Helicopter>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void HandleEnemyHit(Collider2D collision)
    {
        Rigidbody2D targetRb = collision.GetComponent<Rigidbody2D>();
        audioSource.Play();

        if (targetRb != null)
        {
            Vector2 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();

            Vector2 force = pushDirection * Mathf.Clamp(pushForce, 0, maxPushDistance);
            targetRb.AddForce(force, ForceMode2D.Impulse);
            targetRb.velocity = Vector2.ClampMagnitude(targetRb.velocity, 2f);
        }

        collision.GetComponent<EnemyHealth>().TakeDamage(damage);

        if (bulletOfSniper)
        {
            enemyHitCount++;  
            if (enemyHitCount >= 3)  
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);  
        }
    }
}
