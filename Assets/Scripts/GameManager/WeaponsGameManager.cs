using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WeaponsGameManager : MonoBehaviour
{
    private WeaponsData weaponsData; // Dữ liệu vũ khí
    public int baseUpgradePrice = 500;
    public float ratePrice = 0.3f;
    [SerializeField] private GameObject notiPanel;
    [SerializeField] private TMP_Text notiText;

    [SerializeField] private List<WeaponData> weapons; // Danh sách vũ khí 

    private void Start()
    {
        weaponsData = FindAnyObjectByType<WeaponsData>();
        // Load thông tin vũ khí từ PlayerPrefs
        if (weaponsData != null)
        {
            weaponsData.LoadWeaponStates(); // Load dữ liệu vũ khí từ PlayerPrefs
            weapons = weaponsData.weapons; // Gán lại danh sách vũ khí từ WeaponsData
        }
    }

    public void CheckWeaponType(int weaponIndex)
    {
        WeaponData weapon = weapons[weaponIndex];
        string weaponType = weapon.weaponType;
        switch (weaponType)
        {
            case "Pistol":
                UpgradeWeapon(weaponIndex, 10, 20);
                break;
            case "Rifle":
                UpgradeWeapon(weaponIndex, 15, 15);
                break;
            case "Shotgun":
                UpgradeWeapon(weaponIndex, 15, 10);
                break;
            case "Sniper":
                UpgradeWeapon(weaponIndex, 20, 10);
                break;
            default:
                Debug.Log("Weapon type not found!");
                break;
        }
    }


    // Hàm dùng để nâng cấp vũ khí (ví dụ: tăng sát thương và đạn)
    private void UpgradeWeapon(int weaponIndex, int damageIncrease, int ammoIncrease)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Count)
        {
            WeaponData weapon = weapons[weaponIndex];
            bool isOwned = weapon.isOwned;
            int weaponLevel = weapon.Level;

            if (isOwned)
            {
                // Tính giá nâng cấp dựa trên cấp độ hiện tại
                int upgradeCost = Mathf.RoundToInt(baseUpgradePrice * (1 + ratePrice * weaponLevel));
                Debug.Log($"Upgrade cost: {upgradeCost}");

                // Kiểm tra người chơi có đủ tiền hay không
                if (PlayerPrefs.GetInt("PlayerCurrency", 0) >= upgradeCost)
                {
                    // Trừ tiền nâng cấp
                    int currentCurrency = PlayerPrefs.GetInt("PlayerCurrency", 0);
                    PlayerPrefs.SetInt("PlayerCurrency", currentCurrency - upgradeCost);
                    PlayerPrefs.Save();
                    // Tăng chỉ số vũ khí
                    int newDamage = weapon.Damage + damageIncrease;
                    int newAmmo = weapon.Clip + ammoIncrease;
                    weaponLevel++;

                    // Lưu trạng thái mới vào PlayerPrefs
                    weaponsData.SaveWeaponState(weapon, isOwned, newDamage, newAmmo, weaponLevel, weapon.isEquipped);

                    Debug.Log($"Weapon upgraded! New level: {weaponLevel}");
                }
                else
                {
                    int missingCurrency = upgradeCost - PlayerPrefs.GetInt("PlayerCurrency", 0);
                    BuyManager buyManager = FindAnyObjectByType<BuyManager>();
                    if (buyManager != null)
                    {
                        buyManager.ShowPanelNoti(missingCurrency);
                    }
                }
            }
            else
            {
                Debug.Log("Weapon is not owned!");
            }
        }
    }
}
