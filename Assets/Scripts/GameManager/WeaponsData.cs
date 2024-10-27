using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponsData : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();

    private void Start()
    {
        LoadWeaponStates();
       
    }

    // Lưu trạng thái sở hữu, sát thương và số đạn vào PlayerPrefs
    public void SaveWeaponState(WeaponData weapon, bool isOwned, int Damage, int Clip, int Level, bool isEquip)
    {
        string key = weapon.weaponName;
        PlayerPrefs.SetInt(key + "_isOwned", isOwned ? 1 : 0);
        PlayerPrefs.SetInt(key + "_damage", Damage);
        PlayerPrefs.SetInt(key + "_clip", Clip);
        PlayerPrefs.SetInt(key + "_level", Level);
        PlayerPrefs.SetInt(key + "_isEquip", isEquip ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Tải trạng thái sở hữu, sát thương và số đạn từ PlayerPrefs
    public void LoadWeaponStates()
    {
        foreach (var weapon in weapons)
        {
            string key = weapon.weaponName;
            int isOwned = weapon.isOwned ? 1 : 0;
            weapon.isOwned = PlayerPrefs.GetInt(key + "_isOwned", isOwned) == 1;
            weapon.Damage = PlayerPrefs.GetInt(key + "_damage", weapon.Damage);
            weapon.Clip = PlayerPrefs.GetInt(key + "_clip", weapon.Clip);
            weapon.Level = PlayerPrefs.GetInt(key + "_level", weapon.Level);
            int isEquip = weapon.isEquipped ? 1 : 0;
            weapon.isEquipped = PlayerPrefs.GetInt(key + "_isEquip", isEquip) == 1;
        }
    }
}
