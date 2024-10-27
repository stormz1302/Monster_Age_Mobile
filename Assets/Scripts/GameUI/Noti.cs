using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Noti : MonoBehaviour
{
    [SerializeField] private GameObject notiPanel; 
    [SerializeField] private TMP_Text notiText;
    public void CloseNoti()
    {
        notiPanel.SetActive(false);
    }

    public void OpenNoti(string content)
    {
        notiPanel.SetActive(true);
        notiText.text = content;
    }
}
