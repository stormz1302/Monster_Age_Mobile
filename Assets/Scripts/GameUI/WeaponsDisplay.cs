using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Prefab chứa thông tin vũ khí
    [SerializeField] private Transform content;       // Viewport content nơi hiển thị vũ khí
    private List<WeaponData> weapons = new List<WeaponData>(); // Danh sách vũ khí
    private WeaponsData weaponsData; // Thông tin vũ khí


    private void Start()
    {
        // Lấy đối tượng WeaponsData từ scene (hoặc bạn có thể tham chiếu trực tiếp nếu đã gán trong Unity)
        weaponsData = FindObjectOfType<WeaponsData>();

        DisplayWeapons();
    }

    // Hàm hiển thị toàn bộ vũ khí
    public void DisplayWeapons()
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
            int weaponIndex = weapons.IndexOf(weapon);
            // Tạo đối tượng vũ khí từ prefab
            GameObject weaponObject = Instantiate(weaponPrefab, content);
            GunView gunView = weaponObject.GetComponent<GunView>();
            // Lấy thành phần WeaponUI của prefab và gán thông tin vũ khí

            gunView.SetWeaponInfo(weapon, weaponIndex);
        }
    }

   
}
