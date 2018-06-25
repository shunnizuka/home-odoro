using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private FeatureManager mgr;
    private Text descText;

    private List<Button> buttons;
    public Button hair;
    public Button top;
    public Button bottom;

    //color options
    public Color black;
    public Color yellow;
    public Color brown;
    public Color red;
    public Color blue;
    public Color pink;
    private List<Color> colors;
    private int colorindex = 0;

    // Use this for initialization
	void Start () {
        mgr = FindObjectOfType<FeatureManager>();
        descText = transform.Find("Navigation").Find("Text").GetComponent<Text>();
        transform.Find("Navigation").Find("Previous").GetComponent<Button>().onClick.AddListener(() => mgr.PreviousChoice());
        transform.Find("Navigation").Find("Next").GetComponent<Button>().onClick.AddListener(() => mgr.NextChoice());
        InitialiseBtnlist();
        InitialiseColor();
    }

    void InitialiseColor() {
        colors = new List<Color>();
        colors.Add(black);
        colors.Add(yellow);
        colors.Add(brown);
        colors.Add(red);
        colors.Add(blue);
        colors.Add(pink);
    }

    public void UpdateColor(int index)
    {
        colorindex = index;
        mgr.features[mgr.currFeature].SetColor(colors[colorindex]);
    }

    void UpdateFeatureButton()
    {
        for (int i = 0; i < mgr.features.Count; i++)
        {
            buttons[i].transform.Find("Image").GetComponent<Image>().sprite = mgr.features[i].renderer.sprite;
            buttons[i].transform.Find("Image").GetComponent<Image>().color = mgr.features[i].renderer.color;
        }   
    }

	// Update is called once per frame
	void Update () {
        UpdateFeatureButton();
        EventSystem.current.SetSelectedGameObject(buttons[mgr.currFeature].gameObject);
        descText.text = mgr.features[mgr.currFeature].ID + " " + (mgr.features[mgr.currFeature].currIndex + 1);
    }

    void InitialiseBtnlist()
    {
        buttons = new List<Button>();
        hair.onClick.AddListener(() => mgr.SetCurrent(0));
        buttons.Add(hair);
        top.onClick.AddListener(() => mgr.SetCurrent(1));
        buttons.Add(top);
        bottom.onClick.AddListener(() => mgr.SetCurrent(2));
        buttons.Add(bottom);
    }

}
