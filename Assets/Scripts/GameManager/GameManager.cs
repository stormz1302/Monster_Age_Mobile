using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private Transform weaponSlot1;
    [SerializeField] private Transform weaponSlot2;
    private Transform WeaponSlot;
    [SerializeField] private Transform player; 
    private int selectedCharacterIndex; 

    private void Start()
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
        InstantiateCharacters(selectedCharacterIndex);
        
        int equippedWeaponIndex1 = PlayerPrefs.GetInt("_EquipWeapon1", 0);
        int equippedWeaponIndex2 = PlayerPrefs.GetInt("_EquipWeapon2", 0);

        if (equippedWeaponIndex1 >= 0 && equippedWeaponIndex1 < weapons.Count)
        {
            WeaponSlot = weaponSlot1; 
            InstantiateWeapon(equippedWeaponIndex1); 

        }
        else
        {
            Debug.LogError("Equipped weapon index 1 is out of bounds!");
        }

        if (equippedWeaponIndex2 >= 0 && equippedWeaponIndex2 < weapons.Count)
        {
            WeaponSlot = weaponSlot2; 
            InstantiateWeapon(equippedWeaponIndex2); 
        }
        else
        {
            Debug.LogError("Equipped weapon index 2 is out of bounds!");
        }

        WeaponsUI weaponsUI = FindObjectOfType<WeaponsUI>();
        weaponsUI.UpdateWeaponsUI();
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        weaponManager.UpdateWeapons();
    }

    private void InstantiateCharacters(int characterIndex)
    {
        GameObject characterPrefab = characters[characterIndex];
        GameObject instantiatedCharacter = Instantiate(characterPrefab, player.position, Quaternion.identity);
        instantiatedCharacter.transform.SetParent(player);
        Player Player = player.GetComponent<Player>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.animator = instantiatedCharacter.GetComponent<Animator>();
        Player.animator = instantiatedCharacter.GetComponent<Animator>();

    }

    private void InstantiateWeapon(int weaponIndex)
    {
        GameObject weaponPrefab = weapons[weaponIndex];
        GameObject instantiatedWeapon = Instantiate(weaponPrefab, WeaponSlot.position, Quaternion.identity);
        instantiatedWeapon.transform.SetParent(WeaponSlot);
    }

}
