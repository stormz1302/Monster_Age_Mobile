using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private Weapon weapon;
    public GameObject hitEffect;
    public bool isPlayerBullet;

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
        collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        BloodEffect();
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

    private void BloodEffect()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.transform.rotation = transform.rotation;
        Destroy(effect, 0.5f);
    }
}
