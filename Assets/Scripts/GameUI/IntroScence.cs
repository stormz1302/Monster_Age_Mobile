using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScence : MonoBehaviour
{
    [SerializeField] private GameObject Menu ;
    bool firstLoad;
    [SerializeField] private GameObject lightningEffect;
    float LightnigTime = 2f;


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
        InvokeRepeating("LightningEffect", 0.5f, 8f);
    }

    private void Update()
    {
        LightnigTime -= Time.deltaTime;
        if (LightnigTime <= 0)
        {
            lightningEffect.SetActive(false);
            LightnigTime = 2f;
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

    private void LightningEffect()
    {
        lightningEffect.SetActive(true);
    }

}
