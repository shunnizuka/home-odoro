using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanels : MonoBehaviour {

    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject hairPanel;
    public GameObject dressPanel;
    public GameObject wardrobetop;

    public void OpentopPanel ()
    {
        if (CharacterAppearance.atShop)
        {
            topPanel.SetActive(true);
            bottomPanel.SetActive(false);
            hairPanel.SetActive(false);
            wardrobetop.SetActive(false);
        }
        else
        {
            OpenWardrobeTopPanel();
        }
    }

    public void OpenbottomPanel()
    {
        bottomPanel.SetActive(true);
        topPanel.SetActive(false);
        hairPanel.SetActive(false);
        wardrobetop.SetActive(false);
    }

    public void OpenHairPanel()
    {
        hairPanel.SetActive(true);
        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
        wardrobetop.SetActive(false);
    }

    public void OpenWardrobeTopPanel()
    {
        wardrobetop.SetActive(true);
        bottomPanel.SetActive(false);
        topPanel.SetActive(false);
        hairPanel.SetActive(false);
    }
}
