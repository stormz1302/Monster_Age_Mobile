using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponManager weaponManager; // Tham chiếu đến WeaponManager
    public GameObject currentWeapon; // Vũ khí hiện tại đang sử dụng
    public int Ammo; // Số lượng đạn
    [SerializeField] private float fireRate; // Tốc độ bắn đạn
    [SerializeField] private float lifeTime; // Thời gian tồn tại của đạn
    [SerializeField] private float bulletSpeed; // Tốc độ của đạn

    public AudioClip FireSound;

    void OnEnable()
    {
        weaponManager.weapon = currentWeapon.GetComponent<Weapon>();
        weaponManager.FireSound.clip = FireSound;
        Debug.Log(weaponManager.FireSound.clip);
        weaponManager.bulletSpeed = bulletSpeed;
        weaponManager.lifeTime = lifeTime;
        weaponManager.fireRate = fireRate;
    }


}