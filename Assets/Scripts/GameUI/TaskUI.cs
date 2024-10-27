using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
public class TaskData
{
    public string taskName;          // Tên nhiệm vụ
    public string taskDescription;   // Mô tả nhiệm vụ
    public int taskReward;           // Phần thưởng nhiệm vụ
    public bool isCompleted;         // Trạng thái hoàn thành
}

public class TaskUI : MonoBehaviour
{
    [SerializeField] private TaskScriptableObject taskDataSO; // Tham chiếu đến ScriptableObject chứa dữ liệu nhiệm vụ
    [SerializeField] private List<GameObject> lockTask;
    [SerializeField] private List<GameObject> maps;
    public TMP_Text taskTitleText;
    public TMP_Text taskDescriptionText;
    public TMP_Text taskRewardText;
    public TMP_Text taskStatusText;
    public Image StatusImage;
    public bool CompletedPreTask;
    [SerializeField] private Sprite completed;   // Sprite khi hoàn thành nhiệm vụ
    [SerializeField] private Sprite incomplete;  // Sprite khi chưa hoàn thành nhiệm vụ


    private void Start()
    {
        // Tải trạng thái nhiệm vụ từ PlayerPrefs
        LoadTaskState();

        // Hiển thị thông tin của nhiệm vụ đầu tiên nếu có
        if (taskDataSO.tasks.Count > 0)
        {
            OnMapClicked(0);
        }
        CheckTasksAndHideMaps();
    }

    // Hiển thị thông tin nhiệm vụ dựa trên chỉ số nhiệm vụ
    public void DisplayTaskInfo(int taskIndex)
    {
        if (taskIndex > 0 && !taskDataSO.tasks[taskIndex - 1].isCompleted)
        {
            CompletedPreTask = false;
            taskTitleText.text = "Complete the previous task to unlock this one!";
            taskDescriptionText.text = "";
            taskRewardText.text = "";
            StatusImage.sprite = incomplete;
            taskStatusText.text = "Locked";
            return;
        }
        CompletedPreTask = true;
        TaskData task = taskDataSO.tasks[taskIndex];
        taskTitleText.text = "Title: " + task.taskName;
        taskDescriptionText.text = "Description: " + task.taskDescription;
        taskRewardText.text = "Reward: $" + task.taskReward.ToString();

        // Kiểm tra trạng thái hoàn thành từ PlayerPrefs
        bool isCompleted = PlayerPrefs.GetInt(task.taskName, 0) == 1;
        task.isCompleted = isCompleted;

        if (isCompleted)
        {
            StatusImage.sprite = completed;
            taskStatusText.text = "Completed";
        }
        else
        {
            StatusImage.sprite = incomplete;
            taskStatusText.text = "Incomplete";
        }
    }

    private void CheckTasksAndHideMaps()
    {
       
        for (int i = 0; i < taskDataSO.tasks.Count; i++)
        {
            if (i > 0 && !taskDataSO.tasks[i - 1].isCompleted)
            {
                lockTask[i].SetActive(true);
                if (i >= 2 && !taskDataSO.tasks[i - 2].isCompleted)
                {
                    maps[i].SetActive(false);
                }
            }
            else
            {
                lockTask[i].SetActive(false);
            }
        }
    }

    // Hàm được gọi khi người chơi nhấn vào map
    public void OnMapClicked(int mapIndex)
    {
        MapSelect mapSelect = FindObjectOfType<MapSelect>();
        if (mapIndex >= 0 && mapIndex < taskDataSO.tasks.Count)
        {
            DisplayTaskInfo(mapIndex);
            mapSelect.SelectMap(mapIndex);
            PlayerPrefs.SetInt("CurrentTaskIndex", mapIndex);
        }
    }

    // Lưu trạng thái hoàn thành của nhiệm vụ vào PlayerPrefs
    public void SaveTaskState(int taskIndex, bool isCompleted)
    {
        TaskData task = taskDataSO.tasks[taskIndex];
        PlayerPrefs.SetInt(task.taskName, isCompleted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Tải trạng thái hoàn thành của tất cả nhiệm vụ từ PlayerPrefs
    private void LoadTaskState()
    {
        foreach (var task in taskDataSO.tasks)
        {
            task.isCompleted = PlayerPrefs.GetInt(task.taskName, 0) == 1;
        }
    }

    // Lưu trạng thái nhiệm vụ khi thoát ứng dụng
    private void OnApplicationQuit()
    {
        for (int i = 0; i < taskDataSO.tasks.Count; i++)
        {
            SaveTaskState(i, taskDataSO.tasks[i].isCompleted);
        }
    }
}
