using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections;
using System;


public delegate void MyDelegate();
public class WeaponManager : MonoBehaviour
{
    public GameObject weaponSlot1; // Vũ khí trong slot 1
    public GameObject weaponSlot2; // Vũ khí trong slot 2
    public Transform Player;
    [HideInInspector]public GameObject currentWeapon; // Vũ khí hiện tại đang sử dụng
    
    public Animator animator;
    
    public GameObject bulletPrefab; // Prefab của đạn
    public GameObject fireEffectPrefab; // Prefab của hiệu ứng bắn đạn
    public Transform firePoint; // Vị trí bắn đạn
    public float bulletSpeed; // Tốc độ của đạn
    public float fireRate; // Tốc độ bắn đạn
    public float lifeTime; // Thời gian tồn tại của đạn
    
    private bool Firing; // Kiểm tra xem có bắn đạn không
    private bool holdGun;
    private int Ammo;
    private int Ammo1;
    private int Ammo2;

    public AudioSource FireSound; // Âm thanh bắn đạn
    public AudioClip outAmmoClip; // Âm thanh hết đạn

    public WeaponsUI weaponsUI;
    public Weapon weapon;

    private float holdGunTime;
    private float nextFireTime = 0f; // Thời gian bắn đạn tiếp theo
    Transform weapon1;
    Transform weapon2;


    // Khởi tạo vũ khí mặc định hoặc chỉ định vũ khí cho các slot
    private void Start()
    {
        

        
        if (weapon1 != null)
        {
            weaponSlot1.SetActive(true); // Chọn vũ khí cho slot 1
            weaponSlot2.SetActive(false); // Tắt vũ khí cho slot 2
            currentWeapon = weaponSlot1;
            Debug.Log(currentWeapon.gameObject.name);
        }
        else if (weapon1 == null && weapon2 != null)
        {
            weaponSlot2.SetActive(true); // Chọn vũ khí cho slot 2
            weaponSlot1.SetActive(false); // Tắt vũ khí cho slot 1
            currentWeapon = weaponSlot2;
        }
        else
        {
            Debug.LogWarning("No weapon found");
        }

        animator = GetComponent<Animator>();
        FireSound = GetComponent<AudioSource>();

 

        CheckWeaponType();
    }

    public void UpdateWeapons()
    {
        weapon1 = weaponSlot1.transform.GetChild(0);
        weapon2 = weaponSlot2.transform.GetChild(0);
        Ammo1 = weapon1.GetComponent<Weapon>().Ammo;
        Debug.Log(Ammo1);
        Ammo2 = weapon2.GetComponent<Weapon>().Ammo;
        AwakeAmmoUI();
    }

    // Hàm chuyển đổi vũ khí
    private void SwitchWeapon(int slot)
    {
        
        // Tắt vũ khí hiện tại
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Chọn vũ khí dựa trên slot
        switch (slot)
        {
            case 1:
                currentWeapon = weaponSlot1;
                break;
            case 2:
                currentWeapon = weaponSlot2;
                break;
            default:
                Debug.LogWarning("Invalid slot");
                return;
        }

        // Kích hoạt vũ khí hiện tại
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(true);
        }
        CheckWeaponType();
    }
    public void SwitchWeapon1()
    {
        SwitchWeapon(1);
    }
    public void SwitchWeapon2()
    {
        SwitchWeapon(2);
    }

    // Function fire weapon
    private void FireBullet(Quaternion rotation)
    {
        if (_Fire == Sniper) // Chỉ trừ đạn sau khi bắn Sniper
        {
            if (currentWeapon == weaponSlot1)
            {
                Ammo1 -= 1;
                weaponsUI.currentAmmo1 = Ammo1;
            }
            else if (currentWeapon == weaponSlot2)
            {
                Ammo2 -= 1;
                weaponsUI.currentAmmo2 = Ammo2;
            }
            Ammo -= 1;
            weaponsUI.UpdateAmmoUI();
        }

        nextFireTime = Time.time + fireRate;
        FireSound.Play(); // Phát âm thanh bắn đạn
        Quaternion fProtaion = Quaternion.Euler(0, 0, 0);
        if (Player.localScale.x < 0)
        {
            fProtaion = Quaternion.Euler(0, 0, 180);
            Quaternion _rotation = Quaternion.Euler(0, 0, 180);
            rotation = _rotation * rotation;
        }
        GameObject fireEffect = Instantiate(fireEffectPrefab, firePoint.position, fProtaion);
        // Tạo đạn mới tại vị trí của firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        // Thêm logic để đạn di chuyển và xử lý va chạm ở đây
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.right;
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
        Destroy(fireEffect, 0.3f);
        Destroy(bullet, lifeTime);
        weaponsUI.UpdateAmmoUI();

    }

    private void ShotGun()
    {
        if (Time.time >= nextFireTime)
        {
            
            float startAngle = -12.5f;
            for (int i = 0; i < 5; i++)
            {
                Debug.Log(i);
                // Tính toán góc lệch cho từng viên đạn
                float angleOffset = startAngle + (25 / (5 - 1)) * i;
                Quaternion rotation = Quaternion.Euler(0, 0, angleOffset); // Thêm góc lệch vào trục Z
                FireBullet(rotation);
            }
        }
        
        
    }

    public void HoldGun() // Hàm giữ súng với sniper
    {
        if (holdGun)
        {
            animator.speed = 0;
        }
    }

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            FireBullet(rotation);
        }
    }

    private void Pistol()
    {
        Shoot();
    }

    private void Rifle()
    {
        Shoot();
    }

    private void Sniper()
    {
        Debug.Log("Sniper fire");
        animator.SetBool("Sniper", true); 
    }

    private void AwakeAmmoUI()
    {
        
        weaponsUI.currentAmmo1 = Ammo1;
        weaponsUI.currentAmmo2 = Ammo2;
        weaponsUI.UpdateAmmoUI();
    }
    private void CheckWeaponType()
    {
        if (currentWeapon != null)
        {
            Transform Weapon = currentWeapon.transform.GetChild(0);
            Ammo = Weapon.GetComponent<Weapon>().Ammo;
            // Lấy tên của Sprite
            if (weapon != null)
            {
                string weaponType = Weapon.GetComponent<Weapon>().weaponType;
                Debug.Log("weaponType: " + weaponType);
                switch (weaponType)
                {
                    case "Shotgun":
                        _Fire = ShotGun;
                        Debug.Log("Shotgun");
                        break;
                    case "Pistol":
                        _Fire = Pistol;
                        Debug.Log("Pistol");
                        break;
                    case "Rifle":
                        _Fire = Rifle;
                        Debug.Log("Rifle");
                        break;
                    case "Sniper":
                        _Fire = Sniper;
                        Debug.Log("Sniper");
                        break;
                }
            }
        }
        
    }

    private MyDelegate _Fire;
    public void Fire()
    {


        if (Ammo > 0 || _Fire == Sniper) // Sniper vẫn có thể bắn lần đầu nếu có đạn
        {
            if (_Fire != Sniper && currentWeapon == weaponSlot1)
            {
                Ammo1 -= 1;
                weaponsUI.currentAmmo1 = Ammo1;
            }
            else if (_Fire != Sniper && currentWeapon == weaponSlot2)
            {
                Ammo2 -= 1;
                weaponsUI.currentAmmo2 = Ammo2;
            }

            Ammo -= 1;

            if (_Fire != null)
            {
                if (_Fire != Sniper && Time.time >= nextFireTime)
                {
                    Firing = true;
                    animator.SetTrigger("Fire");
                    StartCoroutine(FireRepeatedly()); // Bắt đầu bắn liên tục nếu không phải Sniper
                }
                else if (_Fire == Sniper)
                {
                    holdGun = true;
                    holdGunTime = Time.time + 1f;
                    _Fire(); // Bắn một lần cho Sniper
                }
                Debug.Log("Fire1");
            }
            else
            {
                Debug.LogWarning("No firing method assigned.");
            }
        }
        else
        {
            FireSound.clip = outAmmoClip;
            FireSound.Play();
        }

    }

    private IEnumerator FireRepeatedly()
    {
        while (Firing == true)
        {
            _Fire();
            
            yield return new WaitForSeconds(0f);
        }
    }

    public void StopFire()
    {
        StopCoroutine(FireRepeatedly());
        
        if (_Fire == Sniper)
        {
            if (Time.time >= holdGunTime)
            {
                Shoot();
            }
            holdGun = false;
            animator.speed = 1;
            animator.SetBool("Sniper", false);
        }
        else
        {
            if (Firing == true)
            {
                Firing = false;
                animator.SetTrigger("Fire");
            }
            
        }
         Debug.Log("Stop fire");
    }

    public void AddAmmo(int amountAmmo)
    {
        if(currentWeapon == weaponSlot1)
        {
            Ammo1 += amountAmmo;
            weaponsUI.currentAmmo1 = Ammo1;
        }
        else if (currentWeapon == weaponSlot2)
        {
            Ammo2 += amountAmmo;
            weaponsUI.currentAmmo2 = Ammo2;
        }
        weaponsUI.UpdateAmmoUI();
    }
}
