using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject deployUI;
    [SerializeField] private GameObject Notification;
    [SerializeField] private TMP_Text notitext;
    private TaskUI taskUI;

    
    private void Start()
    {
        taskUI = FindObjectOfType<TaskUI>();
    }   

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
        if (taskUI.CompletedPreTask)
            deployUI.SetActive(true);
        else if (!taskUI.CompletedPreTask)
            Notification.SetActive(true);
            notitext.text = "Complete the previous task to unlock!";
    }
}
