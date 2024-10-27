using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTaskData", menuName = "Tasks/TaskData")]
public class TaskScriptableObject : ScriptableObject
{
    public List<TaskData> tasks; // Danh sách nhiệm vụ
}
