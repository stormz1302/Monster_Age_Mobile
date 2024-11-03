using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployWeapons : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Prefab chứa thông tin vũ khí
    [SerializeField] private Transform content;       // Viewport content nơi hiển thị vũ khí
    private List<WeaponData> weapons = new List<WeaponData>(); // Danh sách vũ khí
    private WeaponsData weaponsData; // Thông tin vũ khí
    [SerializeField] private Image WeaponSlot1;
    [SerializeField] private Image WeaponSlot2;
    [SerializeField] private Outline outline1;
    [SerializeField] private Outline outline2;
    [SerializeField] private GameObject Deploy;
    private int SelectWeaponSlot;


    private void Start()
    {
        // Lấy đối tượng WeaponsData từ scene (hoặc bạn có thể tham chiếu trực tiếp nếu đã gán trong Unity)
        weaponsData = FindObjectOfType<WeaponsData>();

        DisplayDeployWeapons();
        LoadWeaponSlot();
        SelectWeaponSlot1(1);
    }

    // Hàm hiển thị toàn bộ vũ khí
    public void DisplayDeployWeapons()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        if (weaponsData != null)
        {
            weaponsData.LoadWeaponStates(); // Load dữ liệu vũ khí từ PlayerPrefs
            weapons = weaponsData.weapons; // Gán lại danh sách vũ khí từ WeaponsData
        }

        foreach (var weapon in weapons)
        {
            bool isOwned = weapon.isOwned;
            if (isOwned)
            {
                int weaponIndex = weapons.IndexOf(weapon);
                // Tạo đối tượng vũ khí từ prefab
                GameObject weaponObject = Instantiate(weaponPrefab, content);
                DeployView deployView = weaponObject.GetComponent<DeployView>();
                // Lấy thành phần WeaponUI của prefab và gán thông tin vũ khí
                deployView.SetWeaponOwnedInfo(weapon, weaponIndex);
            }
        }
    }

    void LoadWeaponSlot()
    {
        int weaponIndex1 = PlayerPrefs.GetInt("_EquipWeapon1");
        int weaponIndex2 = PlayerPrefs.GetInt("_EquipWeapon2");

        if (weaponIndex1 != -1)
        {
            bool isEquipped = weapons[weaponIndex1].isEquipped;
            if (isEquipped)
            {
                WeaponSlot1.sprite = weapons[weaponIndex1].icon;
                WeaponSlot1.color = new Color(1, 1, 1, 1);
            }
            else
            {
                WeaponSlot1.sprite = null;
                WeaponSlot1.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            foreach (var weapon in weapons)
            {
                if (weapon.isEquipped)
                {
                    weaponIndex1 = weapons.IndexOf(weapon);
                    PlayerPrefs.SetInt("_EquipWeapon1", weaponIndex1);
                    PlayerPrefs.Save();
                    WeaponSlot1.sprite = weapons[weaponIndex1].icon;
                    WeaponSlot1.color = new Color(1, 1, 1, 1);
                    break;
                }
 
            }
        }

        if (weaponIndex2 != -1)
        {
            bool isEquipped = weapons[weaponIndex2].isEquipped;
            if (isEquipped)
            {
                WeaponSlot2.sprite = weapons[weaponIndex2].icon;
                WeaponSlot2.color = new Color(1, 1, 1, 1);
            }
            else
            {
                WeaponSlot2.sprite = null;
                WeaponSlot2.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            foreach (var weapon in weapons)
            {
                if (weapon.isEquipped && weapons.IndexOf(weapon) != weaponIndex1)
                {
                    weaponIndex2 = weapons.IndexOf(weapon);
                    PlayerPrefs.SetInt("_EquipWeapon2", weaponIndex2);
                    PlayerPrefs.Save();
                    WeaponSlot2.sprite = weapons[weaponIndex2].icon;
                    WeaponSlot2.color = new Color(1, 1, 1, 1);
                    break;
                }

            }
        }
    }
    public void DisplayWeaponSlot(int weaponIndex)
    {
        
        WeaponData weapon = weapons[weaponIndex];
        bool isEquipped = weapon.isEquipped;
        Debug.Log(isEquipped);
        Selector selector = FindObjectOfType<Selector>();
        int weaponEquiped = PlayerPrefs.GetInt("_EquipWeapon" + SelectWeaponSlot, -1);
        if (weaponEquiped >= 0)
        {
            selector.UnequipWeapon(SelectWeaponSlot, weaponEquiped);
        }
        switch (SelectWeaponSlot)
        {
            case 1:
                WeaponSlot1.sprite = weapon.icon;
                WeaponSlot1.color = new Color(1, 1, 1, 1);
                selector.EquipWeapon(weaponIndex, SelectWeaponSlot);
                break;
            case 2:
                WeaponSlot2.sprite = weapon.icon;
                WeaponSlot2.color = new Color(1, 1, 1, 1);
                selector.EquipWeapon(weaponIndex, SelectWeaponSlot);
                break;
        }
    }

    public void SelectWeaponSlot1(int weaponSlot)
    {
        SelectWeaponSlot = weaponSlot;
        ToggleOutline(weaponSlot);
    }
    
    private void ToggleOutline(int weaponSlot)
    {
        if (weaponSlot == 1)
        {
            outline1.enabled = true;
            outline2.enabled = false;
        }
        else
        {
            outline1.enabled = false;
            outline2.enabled = true;
        }
    }

    public void QuitDeployWeapons()
    {
        Deploy.SetActive(false);
    }

    public void SwitchWeaponSlots()
    {
        int weaponIndex1 = PlayerPrefs.GetInt("_EquipWeapon1", -1);
        int weaponIndex2 = PlayerPrefs.GetInt("_EquipWeapon2", -1);

        // Hoán đổi vị trí hai vũ khí
        PlayerPrefs.SetInt("_EquipWeapon1", weaponIndex2);
        PlayerPrefs.SetInt("_EquipWeapon2", weaponIndex1);

        // Cập nhật giao diện
        LoadWeaponSlot();
    }
}
