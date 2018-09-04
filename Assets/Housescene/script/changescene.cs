using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour {

    public GameObject FurniturePanel;
    public GameObject MenuPanel;
    public GameObject menuButton;
    public GameObject ButtonPanel;
    public GameObject InsufficientHrPanel;
    public GameObject closeshopButton;
    private bool MenuPanelopened = false;
    private GameObject character;

    public void Start()
    {
        character = GameObject.Find("Character");
    }
    public void change()
    {
        SceneManager.LoadScene(3);
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

        //disable planner, desk, bed buttons
        ButtonPanel.transform.Find("desk").GetComponent<Button>().interactable = false;
        ButtonPanel.transform.Find("bed").GetComponent<Button>().interactable = false;
        ButtonPanel.transform.Find("planner").GetComponent<Button>().interactable = false;
        ButtonPanel.transform.Find("progress chart").GetComponent<Button>().interactable = false;

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

        ButtonPanel.transform.Find("desk").GetComponent<Button>().interactable = true;
        ButtonPanel.transform.Find("bed").GetComponent<Button>().interactable = true;
        ButtonPanel.transform.Find("planner").GetComponent<Button>().interactable = true;
        ButtonPanel.transform.Find("progress chart").GetComponent<Button>().interactable = true;
    }

    public void ToggleMenuPanel()
    {
        MenuPanelopened = !MenuPanelopened;
        if (MenuPanel != null)
        {
            MenuPanel.SetActive(MenuPanelopened);
        }
    }

    public void CloseInsufficientHrPanel()
    {
        InsufficientHrPanel.SetActive(false);
    }
}

