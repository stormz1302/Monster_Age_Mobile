using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private List<CharacterScriptableObject> characters;
    private List<WeaponData> weapons;
    private CharactersData charactersData;
    private WeaponsData weaponsData;
   
    private void Start()
    {
        // lấy dữ liệu nhân vật
        charactersData = GetComponent<CharactersData>();
        charactersData.LoadCharacterData();
        characters = charactersData.characterList;

        // lấy dữ kiệu vũ khí
        weaponsData = GetComponent<WeaponsData>();
        weaponsData.LoadWeaponStates();
        weapons = weaponsData.weapons;
        int selectedCharacterIndex = PlayerPrefs.GetInt("_SelectCharacter", 0);
        SelectCharacter(selectedCharacterIndex);
    }

    public void SelectCharacter(int characterIndex)
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("_SelectCharacter", -1);
        if (selectedCharacterIndex >= 0)
        {
            Debug.Log("Selected character: " + selectedCharacterIndex);
            CharacterScriptableObject selectedCharacter = characters[selectedCharacterIndex];
            charactersData.SaveCharacterOwnership(selectedCharacter, selectedCharacter.isOwned, false);
        }
        CharacterScriptableObject character = characters[characterIndex];
        bool isOwned = character.isOwned;
        if (isOwned)
        {
            Debug.Log("Selecting character: " + characterIndex);
            PlayerPrefs.SetInt("_SelectCharacter", characterIndex);
            PlayerPrefs.Save();
            charactersData.SaveCharacterOwnership(character, isOwned, true);
        }
    }

    

    public void EquipWeapon(int weaponIndex, int WeaponSlot)
    {
        WeaponData weapon = weapons[weaponIndex];
        bool isOwned = weapon.isOwned;
        if (isOwned)
        {
            PlayerPrefs.SetInt("_EquipWeapon" + WeaponSlot, weaponIndex);
            PlayerPrefs.Save();
            weaponsData.SaveWeaponState(weapon, isOwned, weapon.Damage, weapon.Clip, weapon.Level, true);
        }
    }
    public void UnequipWeapon(int WeaponSlot, int weaponIndex)
    {
        WeaponData weapon = weapons[weaponIndex];
        PlayerPrefs.SetInt("_EquipWeapon" + WeaponSlot, -1);
        PlayerPrefs.Save();
        weaponsData.SaveWeaponState(weapon, weapon.isOwned, weapon.Damage, weapon.Clip, weapon.Level, false);
    }
}
