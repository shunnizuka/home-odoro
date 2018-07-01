using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTaskInput : MonoBehaviour
{
    public InputField taskInput;
    public InputField timeInput;

    //to be printed on the button
    private string newTask = "";
    private float hours = 0;

    //things needed to instantiate
    public GameObject canvas;
    public GameObject button;
    public Scroll_View scroll;

    //for Task input field
    public void GetTaskInput(string task)
    {
        newTask = task;
        Debug.Log("Entered " + newTask);
    }

    //for Time input field
    public void GetTimeInput (string time)
    {
        hours = float.Parse(time);
        Debug.Log("Entered " + time);
    }

    // When Add button is clicked use the method in scroll to create button
    public void DisplayInput()
    {
        if (newTask != "")
        {
            scroll.MakeButton(newTask, hours, canvas);
            taskInput.text = "";
            timeInput.text = "";
            newTask = "";
            hours = 0;
        }
    }

}
