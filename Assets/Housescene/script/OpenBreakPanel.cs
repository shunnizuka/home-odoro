using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBreakPanel : MonoBehaviour
{

    public GameObject BreakPanel = null;
    private bool panelopened = false;
    public Timer timer;

    private void Start()
    {

    }

    public void TogglePanel()
    {
        panelopened = !panelopened;
        if (BreakPanel != null)
        {
            BreakPanel.SetActive(panelopened);
            timer.SetboolTask(false);
            Debug.Log("planner open");
        }
    }
}