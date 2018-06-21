using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPlannerPanel : MonoBehaviour
{

    public GameObject PlannerPanel = null;
    private bool panelopened = false;

    private void Start()
    {
        
    }

    public void TogglePanel ()
    {
        panelopened = !panelopened;
        if(PlannerPanel != null)
        {
            PlannerPanel.SetActive(panelopened);
            Debug.Log("planner open");
        }
    }
}