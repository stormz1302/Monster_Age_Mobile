﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator animator;
    private float speed;

    private WeaponManager weaponManager; // Tham chiếu đến WeaponManager
    private JoystickPlayerExample joystick; // Tham chiếu đến JoystickPlayerExample
    private GameObject Weapon;
    public float MeleeTime = 0.25f;

    public float dashCooldown = 2f;
    private float _dashCooldown;

    public bool isDashing = false;
    public float dashSpeed = 50f;
    public float dashTime = 0.1f;
    private float _dashTime;
    public string CharacterName;

    void Start()
    {
        joystick = FindObjectOfType<JoystickPlayerExample>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update()
    {
        speed = joystick.rb.velocity.magnitude;
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
        Sword sword = FindObjectOfType<Sword>();
        sword.OnAttackButtonClicked();
    }


    public void DashButton()
    {

        if (Time.time >= _dashCooldown)
        {
            _dashCooldown = Time.time + dashCooldown;
            animator.SetBool("Dash", true);
            Dash();
        }
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

    public void DisableWeapon()
    {
        Weapon = weaponManager.currentWeapon;
        Weapon.SetActive(false);
    }

    public void CollectItem(Item item)
    {
        ItemManager itemManager = FindObjectOfType<ItemManager>();
        
        switch (item.itemType)
        {
            case Item.ItemType.Health:
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                itemManager.PlaySound(0);
                playerHealth.Heal(item.value);
                break;
            case Item.ItemType.Ammo:
                itemManager.PlaySound(1);
                weaponManager.AddAmmo(item.value);
                break;
            case Item.ItemType.Coin:
                itemManager.PlaySound(2);
                GameManager gameManager = FindObjectOfType<GameManager>();
                gameManager.AddCoins(item.value);
                break;
        }
    }


}
