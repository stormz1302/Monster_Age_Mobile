using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactersView : MonoBehaviour
{
    public TMP_Text characterNameText; // Hiển thị tên nhân vật
    public TMP_Text characterPriceText; // Hiển thị giá nhân vật
    public Image characterImage; // Hiển thị hình ảnh nhân vật
    public Button buyButton; // Nút mua
    public Button selectButton; // Nút chọn
    public TMP_Text selected;
    private int characterIndex; // Chỉ số nhân vật
    private CharacterDisplay characterDisplay;

    private void Start()
    {
        characterDisplay = FindObjectOfType<CharacterDisplay>();
    }

    // Hàm thiết lập thông tin nhân vật cho UI
    public void SetCharacterInfo(CharacterScriptableObject character, int characterIndex)
    {
        characterNameText.text = character.characterName;
        bool isOwned = character.isOwned;
        characterImage.sprite = character.characterImage;
        if (isOwned)
        {
            characterPriceText.text = "";
        }
        else
        {
            characterPriceText.text = "$" + character.price.ToString();
        }
        this.characterIndex = characterIndex;
    }
    public void CheckOwned(CharacterScriptableObject character)
    {
        bool isOwned = character.isOwned;
        bool isSelected = character.isSelected;
        if (isSelected)
        {
            selected.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            selected.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(!isOwned);
            selectButton.gameObject.SetActive(isOwned);
        }
    }

    public void BuyCharacter()
    {
        BuyManager buyManager = FindObjectOfType<BuyManager>();
        buyManager.BuyCharacter(characterIndex);
        characterDisplay.DisplayCharacters();
    }

    public void SelectCharacter()
    {
        Selector selector = FindObjectOfType<Selector>();
        selector.SelectCharacter(characterIndex);
        characterDisplay.DisplayCharacters();
    }
}
