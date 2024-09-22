using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> PathGameUI;


    private void Start()
    {
        for (int i = 0; i < PathGameUI.Count; i++)
        { 
            if (i == 0)
            {
                PathGameUI[i].SetActive(true);
            }
            else
            {
                PathGameUI[i].SetActive(false);
            }
        }
    }
}
