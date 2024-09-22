using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private float speed;

    public WeaponManager weaponManager; // Tham chiếu đến WeaponManager
    public JoystickPlayerExample joystick; // Tham chiếu đến JoystickPlayerExample
    public GameObject Weapons;
    public float MeleeTime = 0.25f;

    public float dashCooldown = 2f;
    private float _dashCooldown;

    public bool isDashing = false;
    public Image DashImage;
    public float dashSpeed = 200f;
    public float dashTime = 0.1f;
    private float _dashTime;

   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        speed = rb.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        
        //giảm speed khi dash
        if (isDashing && Time.time >= _dashTime)
        {
            joystick.speed -= dashSpeed;
            isDashing = false;
            animator.SetBool("Dash", false);
        }

    }

    public void AttackMelee()
    {
        animator.SetBool("Melee", true);
    }
    
    
    public void DashButton()
    {
        
        if (Time.time >= _dashCooldown)
        {
            StartCoroutine(FillDashImage());
            _dashCooldown = Time.time + dashCooldown;
            animator.SetBool("Dash", true);
            Dash();
        }
    }

    IEnumerator FillDashImage()
    {
        float elapsedTime = 0f;
        DashImage.fillAmount = 0f;
        float startFillAmount = DashImage.fillAmount;

        while (elapsedTime < dashCooldown)
        {
            elapsedTime += Time.deltaTime;
            float fillAmount = Mathf.Lerp(startFillAmount, 1f, elapsedTime / dashCooldown);
            DashImage.fillAmount = fillAmount;
            yield return null;
        }

        DashImage.fillAmount = 1f; // Đảm bảo rằng giá trị cuối cùng là 1
    }
    private void Dash()
    {
        if (!isDashing)
        {
            joystick.speed += dashSpeed; 
            _dashTime = Time.time + dashTime;
            isDashing = true; 
        }
        
    }

    public void backMelee()
    {
        animator.SetBool("Melee", false);
        
    }
}
