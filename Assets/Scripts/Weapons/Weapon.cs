using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponManager weaponManager; // Tham chiếu đến WeaponManager
    public GameObject currentWeapon; // Vũ khí hiện tại đang sử dụng
    public int Ammo; // Số lượng đạn
    public int Damage;
    public string weaponType;
    public WeaponData weaponData; 
    [SerializeField] private float fireRate; // Tốc độ bắn đạn
    [SerializeField] private float lifeTime; // Thời gian tồn tại của đạn
    [SerializeField] private float bulletSpeed; // Tốc độ của đạn

    public AudioClip FireSound;

    private void Awake()
    {
        LoadWeaponData();
        Ammo = weaponData.Clip;
        Damage = weaponData.Damage;
        weaponType = weaponData.weaponType;
        if (weaponType == "Shotgun") Damage = Damage / 5;
        Debug.Log("Damage: " + Damage + "," + weaponType);
    }

    void OnEnable()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        weaponManager.weapon = currentWeapon.GetComponent<Weapon>();
        weaponManager.FireSound.clip = FireSound;
        Debug.Log(weaponManager.FireSound.clip);
        weaponManager.bulletSpeed = bulletSpeed;
        weaponManager.lifeTime = lifeTime;
        weaponManager.fireRate = fireRate;
    }

    private void LoadWeaponData()
    {
        string key = weaponData.weaponName;
        weaponData.Damage = PlayerPrefs.GetInt(key + "_damage", weaponData.Damage);
        weaponData.Clip = PlayerPrefs.GetInt(key + "_clip", weaponData.Clip);
    }


}