using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private Transform weaponSlot1;
    [SerializeField] private Transform weaponSlot2;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Transform player;

    public int bonusCoins = 0;

    private Transform WeaponSlot;
    private int selectedCharacterIndex;

    private void Start()
    {
        InitializeCharacter();
        InitializeWeapons();
        InitializeUI();
        UpdateCoinText();
    }

    private void InitializeCharacter()
    {
        if (characters == null || characters.Count == 0)
        {
            Debug.LogError("No characters in the list!");
            return;
        }

        selectedCharacterIndex = PlayerPrefs.GetInt("_SelectCharacter", 0);
        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= characters.Count)
        {
            Debug.LogError("Selected character index is out of bounds!");
            selectedCharacterIndex = 0;
        }

        InstantiateCharacter(selectedCharacterIndex);
    }

    private void InitializeWeapons()
    {
        int equippedWeaponIndex1 = PlayerPrefs.GetInt("_EquipWeapon1", 0);
        int equippedWeaponIndex2 = PlayerPrefs.GetInt("_EquipWeapon2", 0);

        EquipWeapon(equippedWeaponIndex1, weaponSlot1, 1);
        EquipWeapon(equippedWeaponIndex2, weaponSlot2, 2);
    }

    private void EquipWeapon(int weaponIndex, Transform weaponSlot, int slotNumber)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Count)
        {
            WeaponSlot = weaponSlot;
            InstantiateWeapon(weaponIndex);
        }
        else
        {
            Debug.LogError($"Equipped weapon index {slotNumber} is out of bounds!");
        }
    }

    private void InitializeUI()
    {
        WeaponsUI weaponsUI = FindObjectOfType<WeaponsUI>();
        if (weaponsUI != null)
        {
            weaponsUI.UpdateWeaponsUI();
        }

        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.UpdateWeapons();
        }
    }

    private void InstantiateCharacter(int characterIndex)
    {
        GameObject characterPrefab = characters[characterIndex];
        GameObject instantiatedCharacter = Instantiate(characterPrefab, player.position, Quaternion.identity);
        instantiatedCharacter.transform.SetParent(player);

        SetPlayerCharacter(instantiatedCharacter);
    }

    private void SetPlayerCharacter(GameObject instantiatedCharacter)
    {
        Player playerComponent = player.GetComponent<Player>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Animator characterAnimator = instantiatedCharacter.GetComponent<Animator>();

        playerHealth.animator = characterAnimator;
        playerComponent.animator = characterAnimator;
    }

    private void InstantiateWeapon(int weaponIndex)
    {
        GameObject weaponPrefab = weapons[weaponIndex];
        GameObject instantiatedWeapon = Instantiate(weaponPrefab, WeaponSlot.position, Quaternion.identity);
        instantiatedWeapon.transform.SetParent(WeaponSlot);
    }

    public void AddCoins(int value)
    {
        bonusCoins += value;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinsText.text = bonusCoins.ToString();
    }
}
