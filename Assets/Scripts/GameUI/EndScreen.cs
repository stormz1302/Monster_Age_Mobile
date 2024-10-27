using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public static EndScreen Instance { get; private set; }

    [SerializeField] private TaskScriptableObject taskList;
    [SerializeField] private TMP_Text Noti;
    [SerializeField] private TMP_Text MapLevel;
    [SerializeField] private TMP_Text TaskCompleted;
    [SerializeField] private TMP_Text TaskReward;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private Image StateImage;
    [SerializeField] private Sprite completed;   // Sprite khi hoàn thành nhiệm vụ
    [SerializeField] private Sprite incomplete;  // Sprite khi chưa hoàn thành nhiệm vụ
    [SerializeField] private GameObject TaskView;
    [SerializeField] private GameObject TaskDone;
    private int taskIndex;
    private string keyToDelete = "_FirstLoad";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if it already exists
            return; // Exit the method
        }

        Instance = this; // Assign the instance
        DontDestroyOnLoad(gameObject); // Ensure this instance persists across scenes
    }

    private void DisplayMission(int taskIndex)
    {
        int mapLevel = PlayerPrefs.GetInt("SceneIndex") + 1;
        MapLevel.text = "Level: " + mapLevel.ToString();

        TaskData task = taskList.tasks[taskIndex];
        Title.text = "Title: " + task.taskName;
        Description.text = "Description: " + task.taskDescription;
        bool isCompleted = task.isCompleted;
        Debug.Log("Complete: " + isCompleted);
        if (isCompleted)
        {
            TaskCompleted.text = "Mission Completed";
            StartCoroutine(CountUpCoroutine(task.taskReward));
            StateImage.sprite = completed;
        }
        else
        {
            TaskCompleted.text = "Mission Failed";
            TaskReward.text = "Reward: $0";
            StateImage.sprite = incomplete;
        }
    }

    private IEnumerator CountUpCoroutine(int toValue)
    {
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.unscaledDeltaTime; 
            float progress = Mathf.Clamp01(elapsed / 1f);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(0, toValue, progress));
            TaskReward.text = "Reward: $" + currentValue.ToString();
            yield return null;
        }

        TaskReward.text = "Reward: $" + toValue.ToString();
    }

    public void NotiMissionState(bool isCompleted)
    {
        Time.timeScale = 0f;
        taskIndex = PlayerPrefs.GetInt("CurrentTaskIndex");
        TaskDone.SetActive(true);
        if (isCompleted)
        {
            Noti.text = "Mission Completed!";
            taskList.tasks[taskIndex].isCompleted = true;
            PlayerPrefs.SetInt(taskList.tasks[taskIndex].taskName, 1);
            PlayerPrefs.Save();
        }
        else
        {
            Noti.text = "Mission Failed!";
        }
    }

    public void OnNextMissionClicked()
    {
        if (taskIndex < taskList.tasks.Count)
        {
            TaskView.SetActive(true);
            TaskDone.SetActive(false);
            DisplayMission(taskIndex);
        }
        else
        {
            Noti.text = "No more mission!";
        }
    }

    public void NextOnClick()
    {
        Time.timeScale = 1f;
        TaskView.SetActive(false);
        PlayerPrefs.SetString("NextScene", "Menu");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Loading");
    }

    public void RetryOnClick()
    {
        TaskView.SetActive(false);
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("NextScene", currentScene.name);
        SceneManager.LoadScene("Loading");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(keyToDelete);
        PlayerPrefs.Save();
    }
}
