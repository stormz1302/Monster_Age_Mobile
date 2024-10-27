using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScence : MonoBehaviour
{
    [SerializeField] private GameObject Menu ;
    bool firstLoad;


    private void Start()
    {

        firstLoad = PlayerPrefs.GetInt("_FirstLoad", 1) == 1;
        if (firstLoad)
        {
            Menu.SetActive(false);
            firstLoad = false;
            PlayerPrefs.SetInt("_FirstLoad", firstLoad ? 1 : 0);
            PlayerPrefs.Save();
            PlayerPrefs.Save();
        }
        else
        {
            GameReady();
            Debug.Log("Open Menu");
        }

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OpenSetting()
    {
        UIOptionsManager uIOptionsManager = FindObjectOfType<UIOptionsManager>();
        uIOptionsManager.ToggleOptions();
    }

    public void GameReady()
    {
        Menu.SetActive(true);
    }
}
