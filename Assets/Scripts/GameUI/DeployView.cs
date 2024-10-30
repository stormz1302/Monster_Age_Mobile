using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployView : MonoBehaviour
{
    [SerializeField] private TMP_Text weaponNameText; // Gán từ prefab
    [SerializeField] private TMP_Text weaponDamageText; // Gán từ prefab
    [SerializeField] private Image weaponImage; // Gán từ prefab
    [SerializeField] private Button Equip; // Gán từ prefab
    [SerializeField] private GameObject Equiped; // Gán từ prefab
    [SerializeField] private Image levelWeapons;
    [SerializeField] private Sprite[] levelSprites;
    private int weaponIndex; // Chỉ số vũ khí
    private int Level; // Số cấp độ tối đa
    private DeployWeapons deployWeapons;
    private Selector selector;

    private void Start()
    {
        deployWeapons = FindObjectOfType<DeployWeapons>();
    }

    public void SetWeaponOwnedInfo(WeaponData weapon, int weaponIndex)
    {
        weaponNameText.text = weapon.weaponName;
        weaponDamageText.text = weapon.Damage.ToString() + "\n" + weapon.Clip.ToString();
        weaponImage.sprite = weapon.icon;
        this.weaponIndex = weaponIndex;
        bool isEquipped = weapon.isEquipped;
        if (isEquipped)
        {
            Equip.gameObject.SetActive(false);
            Equiped.SetActive(true);
        }
        else
        {
            Equip.gameObject.SetActive(true);
            Equiped.SetActive(false);
        }
        Level = weapon.Level;
        switch (Level)
        {
            case 0:
                levelWeapons.sprite = levelSprites[0];
                break;
            case 1:
                levelWeapons.sprite = levelSprites[1];
                break;
            case 2:
                levelWeapons.sprite = levelSprites[2];
                break;
            case 3:
                levelWeapons.sprite = levelSprites[3];
                break;
        }
    }

    public void EquipWeapon()
    {
        deployWeapons.DisplayWeaponSlot(weaponIndex);
        deployWeapons.DisplayDeployWeapons();
    }
}
