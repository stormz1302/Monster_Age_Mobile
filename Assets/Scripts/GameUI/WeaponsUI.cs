using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsUI : MonoBehaviour
{
    public Image ImageWeapon1;
    public Image ImageWeapon2;

    public WeaponManager weaponManager;

    public int currentAmmo1;
    public int currentAmmo2;

    public TextMeshProUGUI AmmoText1;
    public TextMeshProUGUI AmmoText2;

    public void Start()
    {
        UpdateWeaponsUI();
        
    }

    private void UpdateWeaponsUI()
    {
        Transform weapon1 = weaponManager.weaponSlot1.transform.GetChild(0);
        Transform weapon2 = weaponManager.weaponSlot2.transform.GetChild(0);

        if (weapon1 != null )
        {
            ImageWeapon1.sprite = weapon1.GetComponent<SpriteRenderer>().sprite;

        }
        if (weapon2 != null)
        {
            ImageWeapon2.sprite = weapon2.GetComponent<SpriteRenderer>().sprite;
        }
        
    }

    public void UpdateAmmoUI()
    {
        
        AmmoText1.text = currentAmmo1.ToString();
        AmmoText2.text = currentAmmo2.ToString();
        
        

    }
}
