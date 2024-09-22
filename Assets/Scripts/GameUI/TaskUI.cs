using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class TaskData
{
    public string taskName;
    public string taskDescription;
    public int taskReward;
    public bool isCompleted;
}


public class TaskUI : MonoBehaviour
{
    public List<TaskData> tasks = new List<TaskData>();

    public TMP_Text taskTitleText;
    public TMP_Text taskDescriptionText;
    public TMP_Text taskRewardText;
    public TMP_Text taskStatusText;
    public Image StatusImage;
    [SerializeField] private Sprite completed;
    [SerializeField] private Sprite incomplete;


    private void Start()
    {
        // Hiển thị thông tin nhiệm vụ đầu tiên
        DisplayTaskInfo(tasks[0].taskName, tasks[0].taskDescription, tasks[0].taskReward, tasks[0].isCompleted);
    }
    // Hàm lấy nhiệm vụ tương ứng với chỉ số của map
    public TaskData GetTaskForMap(int mapIndex)
    {
        if (mapIndex >= 0 && mapIndex < tasks.Count)
        {
            return tasks[mapIndex];
        }
        return null;
    }

    // Hiển thị thông tin nhiệm vụ ra giao diện UI
    public void DisplayTaskInfo(string title, string description, int reward, bool isCompleted)
    {
        

        taskTitleText.text = "Title: " + title;
        taskDescriptionText.text = "Description: " + description;
        taskRewardText.text = " $" + reward.ToString();
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

    // Hàm được gọi khi người chơi nhấn vào map
    public void OnMapClicked(int mapIndex)
    {
        TaskData taskData = GetTaskForMap(mapIndex);
        if (taskData != null)
        {
            // Hiển thị thông tin nhiệm vụ
            DisplayTaskInfo(taskData.taskName, taskData.taskDescription, taskData.taskReward, taskData.isCompleted);
        }
    }
}
