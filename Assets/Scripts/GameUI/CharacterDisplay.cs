using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab; // Prefab chứa thông tin nhân vật
    [SerializeField] private Transform content; // Content nơi hiển thị nhân vật
    private List<CharacterScriptableObject> characters = new List<CharacterScriptableObject>(); // Danh sách nhân vật
    private CharactersData charactersData; // Dữ liệu nhân vật

    private void Start()
    {
        // Lấy đối tượng CharactersData từ scene
        charactersData = FindObjectOfType<CharactersData>();

        DisplayCharacters();
    }

    // Hiển thị toàn bộ nhân vật
    public void DisplayCharacters()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        if (charactersData != null)
        {
            charactersData.LoadCharacterData(); // Load dữ liệu nhân vật từ PlayerPrefs
            characters = charactersData.characterList; // Gán lại danh sách nhân vật từ CharactersData
        }
        foreach (var character in characters)
        {
            int characterIndex = characters.IndexOf(character);
            // Tạo đối tượng nhân vật từ prefab
            GameObject characterObject = Instantiate(characterPrefab, content);

            // Lấy thành phần CharacterView của prefab và gán thông tin nhân vật
            CharactersView characterView = characterObject.GetComponent<CharactersView>();
            characterView.SetCharacterInfo(character, characterIndex); // Gán thông tin cho character
            characterView.CheckOwned(character); // Kiểm tra trạng thái sở hữu của nhân vật
        }
    }
}
