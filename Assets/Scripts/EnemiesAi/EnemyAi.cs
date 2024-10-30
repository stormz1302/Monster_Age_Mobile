using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using CitrioN.SettingsMenuCreator;


public class EnemyAi : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed;
    public float nextWaypointDistance;
    public Animator animator;

    public Seeker seeker;
    public bool updateContinuesPath;
    public bool canMove; 
    public bool isShootable;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float shootRate;
    private float shootCooldown;
    private Vector2 previousPosition;
    private float distanceToPlayer;
    private LoneSurvivor loneSurvivor;
    bool reachDestination;
    Path path;
    Coroutine moveCoroutine;

    private void Start()
    {
        canMove = true;
        loneSurvivor = FindObjectOfType<LoneSurvivor>();
        if (!roaming) InvokeRepeating("CalculatePath", 0f, 0.5f);

        reachDestination = true;
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (!canMove) return;
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0 && isShootable)
        {
            shootCooldown = shootRate;
            EnemyShoot();

        }

        float currentSpeed = ((Vector2)transform.position - previousPosition).magnitude / Time.deltaTime;

        previousPosition = transform.position;
        animator.SetFloat("Speed", currentSpeed);
        if (roaming)
        {
            Vector2 playerPos = FindAnyObjectByType<Player>().transform.position;
            distanceToPlayer = Vector2.Distance(transform.position, playerPos);
            if (distanceToPlayer >= 12f)
            {
                CalculatePath();
            }
        }
    }

    void EnemyShoot()
    {
        var bullletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullletTmp.GetComponent<Rigidbody2D>();
        Vector3 direction;
        if (loneSurvivor != null && loneSurvivor.loneSurvivor)
        {
            Vector3 helicopterPos = FindAnyObjectByType<Helicopter>().transform.position;
            direction = helicopterPos - transform.position;
        }
        else
        {
            Vector3 playerPos = FindAnyObjectByType<Player>().transform.position;
            direction = playerPos - transform.position;
        }
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
        while (currentWaypoint < path.vectorPath.Count)
        {
            if (!canMove) yield break;
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            // Xoay enemy theo hướng di chuyển
            if (direction.x != 0)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = direction.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }

            // Kiểm tra khoảng cách tới điểm đến
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
        if(loneSurvivor != null && loneSurvivor.loneSurvivor)
        {
            Vector2 helicopterPos = FindAnyObjectByType<Helicopter>().transform.position;
            if (helicopterPos != null)
            {
                if (roaming)
                {
                    return helicopterPos + Random.Range(10f, 12f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                }
                else
                {
                    return helicopterPos;
                }
            }
        }
        else
        {
            Vector2 playerPos = FindAnyObjectByType<Player>().transform.position;
            if (roaming)
            {

                return playerPos + Random.Range(10f, 12f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            else
            {
                return playerPos;
            }
        }
        return Vector2.zero;
    }
}
