using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunView : MonoBehaviour
{
    [SerializeField] private TMP_Text weaponNameText; // Gán từ prefab
    [SerializeField] private TMP_Text weaponPriceText; // Gán từ prefab
    [SerializeField] private TMP_Text weaponDamageText; // Gán từ prefab
    [SerializeField] private Image weaponImage; // Gán từ prefab
    [SerializeField] private Button buyButton; // Gán từ prefab
    [SerializeField] private Button upgradeButton; // Gán từ prefab
    [SerializeField] private Image levelWeapons;
    [SerializeField] private Sprite[] levelSprites;
    [SerializeField] private TMP_Text max;
    

    private int weaponIndex; // Chỉ số vũ khí
    private int Level = 0; // Số cấp độ tối đa
    private WeaponsDisplay weaponsDisplay;

    private void Start()
    {
        weaponsDisplay = FindObjectOfType<WeaponsDisplay>();

    }

    public void SetWeaponInfo(WeaponData weapon, int weaponIndex)
    {
        weaponNameText.text = weapon.weaponName;
        Level = weapon.Level;
        if (weapon.isOwned)
        {
            if (Level < 3)
            {
                WeaponsGameManager weaponsGameManager = FindObjectOfType<WeaponsGameManager>();
                int upgradeCost = Mathf.RoundToInt(weaponsGameManager.baseUpgradePrice * (1 + weaponsGameManager.ratePrice * Level));
                weaponPriceText.text = "$" + upgradeCost.ToString();
            }
            else
            {
                weaponPriceText.text = "";
            }
        }
        else
        {
            weaponPriceText.text = "$" + weapon.Price.ToString();
        }
        weaponDamageText.text = weapon.Damage.ToString() + "\n" + weapon.Clip.ToString();
        weaponImage.sprite = weapon.icon;
        this.weaponIndex = weaponIndex;
        
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
        CheckOwned(weapon);
    }

    private void CheckOwned(WeaponData weapon)
    {
        bool isOwned = weapon.isOwned;
        buyButton.gameObject.SetActive(!isOwned && Level < 3);
        upgradeButton.gameObject.SetActive(isOwned && Level < 3);
        max.gameObject.SetActive(Level == 3);
    }

    public void BuyWeapon()
    {

        BuyManager buyManager = FindObjectOfType<BuyManager>();
        buyManager.BuyWeapon(weaponIndex);
         
        weaponsDisplay.DisplayWeapons();
    }

    public void UpgradeWeapon()
    {
        Debug.Log("Weapon is max level!" + Level);
        if (Level < 3)
        {
            WeaponsGameManager weaponsGameManager = FindObjectOfType<WeaponsGameManager>();
            weaponsGameManager.CheckWeaponType(weaponIndex);
            Debug.Log("Weapon level: " + Level);
            weaponsDisplay.DisplayWeapons();
        }
    }
}
