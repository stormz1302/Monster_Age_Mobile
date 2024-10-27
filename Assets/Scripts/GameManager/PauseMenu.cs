using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI; 

    

    // Hàm Resume game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);       
        Time.timeScale = 1f;               
        isGamePaused = false;              
    }

    // Hàm Pause game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);        
        Time.timeScale = 0f;              
        isGamePaused = true;               
    }

    // Nút Retry để khôi phục lại trò chơi
    public void RetryGame()
    {
        Time.timeScale = 1f;                
        Scene currentScene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(currentScene.name); 
    }

    // Nút Quit để trở về Menu chính
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;               
        PlayerPrefs.SetString("NextScene", "Menu"); 
        SceneManager.LoadScene("Loading"); 
    }

    public void OpenOptions()
    {
        UIOptionsManager uIOptionsManager = FindObjectOfType<UIOptionsManager>();
        uIOptionsManager.ToggleOptions();
    }
}
