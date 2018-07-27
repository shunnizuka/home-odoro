using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtons : MonoBehaviour {

    public GameObject Button;
 
    //variables to make button
    private string task;
    private float time;
    public GameObject field;

    //data stuff
    public ListofTask tasks;
    public string path;
    private string jsonString;

    private void Start()
    {
        path = Application.persistentDataPath + "/ListOfTask.json";
        if(System.IO.File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
            tasks = JsonUtility.FromJson<ListofTask>(jsonString);
            Debug.Log("Task file exists" + jsonString);
        }
        else
        {
            tasks.taskList = new List<Task>();
        }
    }

    private void LoadButtons ()
    {
        for( int i = 0; i < tasks.taskList.Count; i++)
        {
            MakeTaskButton(tasks.taskList[i].taskInfo, tasks.taskList[i].time);
        }
    }

    private void Save()
    {
        string newTaskData = JsonUtility.ToJson(tasks);
        File.WriteAllText(path, newTaskData);
        Debug.Log(newTaskData);
    }

    public void AddNewTask (string newTask, float hour)
    {
        Task newtask = new Task(newTask, hour);
        tasks.taskList.Add(newtask);
        Save();
        MakeTaskButton(newTask, hour);
    }

    public void MakeTaskButton(string newTask, float hours)
    { 
        GameObject newButton = Instantiate(Button) as GameObject;
        newButton.transform.Find("Text").GetComponentInChildren<Text>().text = newTask.ToUpper();
        newButton.transform.Find("hour").GetComponentInChildren<Text>().text = "Time: " + hours + " hours    ";
        newButton.transform.SetParent(field.transform, false);

        Debug.Log("Entered" + task + "display and button made");
    }
}

[System.Serializable]
public class ListofTask
{
    public List<Task> taskList;
}

[System.Serializable]
public class Task
{
    public string taskInfo;
    public float time;

    public Task (string info, float hr)
    {
        taskInfo = info;
        time = hr;
    }

}
