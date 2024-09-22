using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAi : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed;
    public float nextWaypointDistance;

    public Seeker seeker;
    public bool updateContinuesPath;

    public bool isShootable = true;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float shootRate;
    private float shootCooldown;

    bool reachDestination;
    Path path;
    Coroutine moveCoroutine;

    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    private void Update()
    {
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0 && isShootable)
        {
            shootCooldown = shootRate;
            EnemyShoot();
        }
        
    }

    void EnemyShoot()
    {
        var bullletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindAnyObjectByType<Player>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullletTmp, bulletLifeTime);
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if (seeker.IsDone() && reachDestination || updateContinuesPath)
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
        }
    }   

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWaypoint = 0;
        reachDestination = false;
        while (currentWaypoint < path.vectorPath.Count  )
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            yield return null;
        }

        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        Vector2 playerPos = FindAnyObjectByType<Player>().transform.position;
        if (roaming)
        {
            return playerPos + Random.Range(10f, 15f)* new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        else
        {
            return playerPos;
        }
    }
}
