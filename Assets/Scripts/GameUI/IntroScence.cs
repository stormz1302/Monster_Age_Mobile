using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScence : MonoBehaviour
{
    [SerializeField] private GameObject Options ;
    [SerializeField] private GameObject Menu ;


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
        Options.SetActive(true);
    }

    public void GameReady()
    {
        Menu.SetActive(true);
    }
}
