using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    public Transform player;             
    public float minDistance = 3f;       
    public float maxDistance = 5f;       
    public float moveSpeed = 2f;         

    public GameObject bulletPrefab;      
    public Transform firePoint;          
    public float fireRate = 1f;          
    public float bulletSpeed = 10f;      

    private float nextFireTime = 0f;     
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MaintainDistanceFromPlayer();
        Transform closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            AimAndShoot(closestEnemy);
        }
    }
 
    void MaintainDistanceFromPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < minDistance)
        {
            Vector2 moveDir = (transform.position - player.position).normalized;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer > maxDistance)
        {
            Vector2 moveDir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    void AimAndShoot(Transform enemy)
    {
        Vector2 direction = enemy.position - transform.position;

        if (Mathf.Abs(direction.y) < 1f && Time.time >= nextFireTime)
        {
            Shoot(direction.normalized);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
