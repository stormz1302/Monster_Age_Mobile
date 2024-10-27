using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    [SerializeField] private GameObject shopWeapons;
    [SerializeField] private Button WeaponsButton;
    [SerializeField] private GameObject shopCharacters;
    [SerializeField] private Button CharactersButton;

    private Color weaponsbutton;
    private Color charactersbutton;

    private void Start()
    {
        weaponsbutton = WeaponsButton.GetComponent<Image>().color;
        charactersbutton = CharactersButton.GetComponent<Image>().color;
        openShopWeapons();
    }

    public void openShopWeapons()
    {
        shopWeapons.SetActive(true);
        weaponsbutton.a = 1;
        WeaponsButton.GetComponent<Image>().color = weaponsbutton;
        shopCharacters.SetActive(false);
        charactersbutton.a = 0.5f;
        CharactersButton.GetComponent<Image>().color = charactersbutton;
    }

    public void openShopCharacters()
    {
        shopWeapons.SetActive(false);
        weaponsbutton.a = 0.5f;
        WeaponsButton.GetComponent<Image>().color = weaponsbutton;
        shopCharacters.SetActive(true);
        charactersbutton.a = 1;
        CharactersButton.GetComponent<Image>().color = charactersbutton;
    }   

    public void closeShop()
    {
        gameObject.SetActive(false);
    }
}
