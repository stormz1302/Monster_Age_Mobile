using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharactersData : MonoBehaviour
{
    public List<CharacterScriptableObject> characterList; // Danh sách các nhân vật từ ScriptableObject


    private void Start()
    {
        LoadCharacterData(); // Tải dữ liệu sở hữu khi bắt đầu
    }

    // Hàm để lưu trạng thái sở hữu của nhân vật vào PlayerPrefs
    public void SaveCharacterOwnership(CharacterScriptableObject character, bool isOwned, bool isSelected)
    {
        Debug.Log("Save character ownership" + isOwned);
        string key = character.characterName;
        PlayerPrefs.SetInt(key + "_isOwned", isOwned ? 1 : 0);
        PlayerPrefs.SetInt(key + "_isSelected", isSelected ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Hàm để tải trạng thái sở hữu của nhân vật từ PlayerPrefs
    public void LoadCharacterData()
    {
        foreach (var character in characterList)
        {
            string key = character.characterName;
            int isOwned = character.isOwned ? 1 : 0;
            character.isOwned = PlayerPrefs.GetInt(key + "_isOwned", isOwned) == 1;
            int isSelected = character.isSelected ? 1 : 0;
            character.isSelected = PlayerPrefs.GetInt(key + "_isSelected", isSelected) == 1;
        }
    }
   
}
