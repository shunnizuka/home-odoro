using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPlannerPanel : MonoBehaviour
{

    public GameObject PlannerPanel = null;
    private bool panelopened = false;
    public Timer timer;

    private void Start()
    {
        
    }

    public void TogglePanel ()
    {
        panelopened = !panelopened;
        if(PlannerPanel != null)
        {
            PlannerPanel.SetActive(panelopened);
            timer.SetboolTask(true);
            Debug.Log("planner open");
        }
    }
}