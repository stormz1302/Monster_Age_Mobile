using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject deployUI;
    public void QuitMenu()
    {
        gameObject.SetActive(false);
    }

    public void openShop()
    {
        shopUI.SetActive(true);
    }
    public void openDeploy()
    {
        deployUI.SetActive(true);
    }
}
