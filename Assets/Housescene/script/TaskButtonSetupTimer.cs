using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TaskButtonSetupTimer : MonoBehaviour
{
    private GameObject timerPanel;
    private GameObject parent;
    private bool panelopened = false;

    //opens up timer panel
    public void openTimer()
    {
        //look for the timer panel to open
        parent = GameObject.Find("PanelCanvas");
        timerPanel = parent.transform.Find("TimerPanel").gameObject;
        Debug.Log("found panel");
        //activate panel
        panelopened = !panelopened;
        if (timerPanel != null)
        {
            timerPanel.SetActive(panelopened);
        }

        //pass on the time to the timer
        string hourtext = this.transform.Find("hour").GetComponentInChildren<Text>().text;
        string hourOnly = Regex.Replace(hourtext, @"[^0-9\.]+", "");
        Debug.Log("Time: " + hourOnly +" End");
        //get the timer script from the panel to assign the time
        Timer script = timerPanel.GetComponent<Timer>();
        script.SetTime(float.Parse(hourOnly));

    }
    

}
