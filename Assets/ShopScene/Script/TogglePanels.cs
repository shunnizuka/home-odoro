using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanels : MonoBehaviour {

    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject hairPanel;
    public GameObject dressPanel;

    public void OpentopPanel ()
    {
        topPanel.SetActive(true);
        bottomPanel.SetActive(false);
        hairPanel.SetActive(false);
    }

    public void OpenbottomPanel()
    {
        bottomPanel.SetActive(true);
        topPanel.SetActive(false);
        hairPanel.SetActive(false);
    }

    public void OpenHairPanel()
    {
        hairPanel.SetActive(true);
        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
    }

}
