using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour {

    public GameObject FurniturePanel;
    public GameObject MenuPanel;
    public GameObject menuButton;
    public GameObject closeshopButton;
    private bool MenuPanelopened = false;
    private GameObject character;

    public void Start()
    {
        character = GameObject.Find("Character");
    }
    public void change()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenFurnitureShop()
    {
        FurniturePanel.SetActive(true);
        MenuPanel.SetActive(false);
        menuButton.SetActive(false);
        closeshopButton.SetActive(true);
        character.transform.Find("hair").gameObject.SetActive(false);
        character.transform.Find("Top").gameObject.SetActive(false);
        character.transform.Find("bottom").gameObject.SetActive(false);
        character.transform.Find("face").gameObject.SetActive(false);
        character.transform.Find("body").gameObject.SetActive(false);

    }

    public void CloseFurnitureShop()
    {
        FurniturePanel.SetActive(false);
        menuButton.SetActive(true);
        character.transform.Find("hair").gameObject.SetActive(true);
        character.transform.Find("Top").gameObject.SetActive(true);
        character.transform.Find("bottom").gameObject.SetActive(true);
        character.transform.Find("face").gameObject.SetActive(true);
        character.transform.Find("body").gameObject.SetActive(true);
        closeshopButton.SetActive(false);
        Debug.Log("close furniture shop");
    }

    public void ToggleMenuPanel()
    {
        MenuPanelopened = !MenuPanelopened;
        if (MenuPanel != null)
        {
            MenuPanel.SetActive(MenuPanelopened);
        }
    }
}

