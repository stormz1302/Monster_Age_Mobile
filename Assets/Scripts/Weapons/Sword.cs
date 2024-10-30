using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Để làm việc với UI

public class Sword : MonoBehaviour
{
    public int damage = 10;  
    public float attackCooldown = 1f;
    public AudioSource audioSource;
    private float lastAttackTime;  


    [SerializeField] private Collider2D swordCollider; 


    private void Start()
    {
        lastAttackTime = -attackCooldown;  
        swordCollider.enabled = false;  
    }

    // Gọi khi nhấn nút tấn công
    public void OnAttackButtonClicked()
    {
        
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Player player = FindObjectOfType<Player>();
            player.animator.SetBool("Melee", true);
            audioSource.Play();
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;  
        }
    }

    IEnumerator PerformAttack()
    {
        swordCollider.enabled = true;  
        yield return new WaitForSeconds(0.5f);  
        swordCollider.enabled = false; 
    }

    // Khi kiếm chạm vào đối tượng enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  
            }
        }
    }
}
