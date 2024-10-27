using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    
    [SerializeField] private List<string> sceneNames;
    [SerializeField] private List<Outline> outlines;
    private string selectedMap;

    private void Start()
    {
        PlayerPrefs.SetString("NextScene", sceneNames[0]);
    }
    public void LoadScene()
    {
        int weaponIndex1 = PlayerPrefs.GetInt("_EquipWeapon1", -1);
        int weaponIndex2 = PlayerPrefs.GetInt("_EquipWeapon2", -1);
        Noti noti = FindObjectOfType<Noti>();
        if (weaponIndex1 >= 0 && weaponIndex2 >= 0)
        {
            SceneManager.LoadScene("Loading");
        }
        else if (weaponIndex1 == -1 )
        {
            noti.OpenNoti("Please select weapon 1");
        }else if (weaponIndex2 == -1 )
        {
            noti.OpenNoti("Please select weapon 2");
        }
        else
        {
            noti.OpenNoti("Please select weapons");
        }

    }

    public void SelectMap(int secnceIndex)
    {
        selectedMap = sceneNames[secnceIndex];
        PlayerPrefs.SetString("NextScene", selectedMap);
        PlayerPrefs.SetInt("SceneIndex", secnceIndex);
        OutLine(secnceIndex);
    }

    private void OutLine(int index)
    {
        for (int i = 0; i < outlines.Count; i++)
        {
            if (i == index)
            {
                outlines[i].enabled = true;
            }
            else
            {
                outlines[i].enabled = false;
            }
        }
    }
 
}
