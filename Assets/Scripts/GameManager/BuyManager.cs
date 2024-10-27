using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private CharactersData charactersData; // Tham chiếu đến danh sách nhân vật
    [SerializeField] private WeaponsData weaponsData;       // Tham chiếu đến danh sách vũ khí
    [SerializeField] private GameObject notiPanel;
    [SerializeField] private TMP_Text notiText;
    private int playerCurrency;

    private void Start()
    {
        playerCurrency = PlayerPrefs.GetInt("PlayerCurrency", 0);
    }

    // Mua nhân vật
    public void BuyCharacter(int characterIndex)
    {
        CharacterScriptableObject character = charactersData.characterList[characterIndex];
        Debug.Log("Buy:" + character.characterName);
        if (!character.isOwned)
        {
            Debug.Log("Character not owned.");
            if (CanAfford(character.price))
            {
                Debug.Log("Character can be bought.");
                DeductCurrency(character.price);
                character.isOwned = true;

                // Lưu trạng thái mua vào PlayerPrefs
                charactersData.SaveCharacterOwnership(character, true, character.isSelected);
            }
            else
            {
                ShowPanelNoti(character.price - playerCurrency);
                Debug.Log("Not enough currency to buy this character.");
            }
        }
        else
        {
            Debug.Log("Character already owned.");
        }
    }

    // Mua vũ khí
    public void BuyWeapon(int weaponIndex)
    {
        WeaponData weapon = weaponsData.weapons[weaponIndex];

        if (!weapon.isOwned)
        {
            if (CanAfford(weapon.Price))
            {
                DeductCurrency(weapon.Price);
                weapon.isOwned = true;

                // Lưu trạng thái mua vào PlayerPrefs
                weaponsData.SaveWeaponState(weapon, true, weapon.Damage, weapon.Clip, weapon.Level, weapon.isEquipped);
            }
            else
            {
                ShowPanelNoti(weapon.Price - playerCurrency);
                Debug.Log("Not enough currency to buy this weapon.");
            }
        }
        else
        {
            Debug.Log("Weapon already owned.");
        }
    }

    // Kiểm tra xem người chơi có đủ tiền để mua hay không
    private bool CanAfford(int price)
    {
        return playerCurrency >= price;
    }

    // Trừ tiền sau khi mua vật phẩm
    private void DeductCurrency(int amount)
    {
        Debug.Log("Deducting " + amount + " from player currency.");
        PlayerPrefs.SetInt("PlayerCurrency", playerCurrency - amount);
        PlayerPrefs.Save();
    }

    // Hàm kiểm tra xem nhân vật đã được mua chưa (từ PlayerPrefs)
    public bool IsCharacterOwned(int characterIndex)
    {
        CharacterScriptableObject character = charactersData.characterList[characterIndex];
        return PlayerPrefs.GetInt(character.characterName + "_Owned", 0) == 1;
    }

    // Hàm kiểm tra xem vũ khí đã được mua chưa (từ PlayerPrefs)
    public bool IsWeaponOwned(int weaponIndex)
    {
        WeaponData weapon = weaponsData.weapons[weaponIndex];
        return PlayerPrefs.GetInt(weapon.weaponName + "_Owned", 0) == 1;
    }

    public void ShowPanelNoti(int missingAmount)
    {
        notiPanel.SetActive(true);
        notiText.text = "You need " + missingAmount.ToString() + " more coins!";
    }
}
