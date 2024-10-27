using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public TMP_Text loadingText;  
    public Image progressBar;     
    private string sceneToLoad;   

    void Start()
    {
        
        sceneToLoad = PlayerPrefs.GetString("NextScene");
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false; 

        while (!asyncOperation.isDone)
        {
           
            float progress = asyncOperation.progress / 0.9f;
            loadingText.text = $"Loading... {(progress * 100f):F0}%";
            progressBar.fillAmount = progress;

            
            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.text = "Almost done..."; 
                yield return new WaitForSeconds(1f); 

                
                asyncOperation.allowSceneActivation = true;
            }

            yield return null; 
        }
    }
}
