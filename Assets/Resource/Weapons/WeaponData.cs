using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;       // Tên vũ khí
    public string weaponType;
    public int Price;           // Giá cơ bản của vũ khí
    public Sprite icon;             // Hình ảnh vũ khí
    public int Damage;          // Sát thương cơ bản
    public int Clip;            // Số đạn cơ bản
    public int Level;
    public bool isEquipped;
    public bool isOwned;            // Trạng thái sở hữu
    public GameObject prefab;       // Prefab vũ khí
}
