
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakButton : MonoBehaviour
{

    public GameObject Button;
    private bool ButtonMade = false;

    //variables to make button
    private string task;
    private float time;
    private GameObject field;

    void Update()
    {
        if (ButtonMade)
        {
            GameObject newButton = Instantiate(Button) as GameObject;
            newButton.transform.Find("Text").GetComponentInChildren<Text>().text = task.ToUpper();
            newButton.transform.Find("hour").GetComponentInChildren<Text>().text = "Time: " + time + " hours    ";
            newButton.transform.SetParent(field.transform, false);

            Debug.Log("Entered" + task + "display and button made");
            ButtonMade = false;
        }
    }

    public void MakeButton(string newTask, float hours, GameObject canvas)
    {
        task = newTask;
        time = hours;
        field = canvas;
        ButtonMade = true;
    }

}
