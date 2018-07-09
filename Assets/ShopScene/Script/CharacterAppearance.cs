using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterAppearance : MonoBehaviour
{
    public static bool atShop = true;

    private FeatureManager shopMgr;

    //navigation buttons
    private List<Button> buttons;
    public Button hair;
    public Button top;
    public Button bottom;
    public Button dress;

    //wardrobe/shop button
    public Sprite wardrobeSprite;
    public Sprite shopSprite;
    public Text buttonText;
    public Button wardrobeBtn;

    //hair shop items to change color
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public TogglePanels toggle;
    public GenerateShopItems generate;

    private void Start()
    {
        shopMgr = FindObjectOfType<FeatureManager>();
        // shopMgr.LoadFeature();
        List<Feature> features = new List<Feature>();
        GameObject character = GameObject.Find("Character");
        features.Add(new Feature("top", character.transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", character.transform.Find("bottom").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("hair", character.transform.Find("hair").GetComponent<SpriteRenderer>()));
        shopMgr = new FeatureManager(features, shopMgr.colors);

        UpdateShopOrWardrobe("shop_");

        button1.GetComponent<Image>().color = shopMgr.features[2].renderer.color;
        button2.GetComponent<Image>().color = shopMgr.features[2].renderer.color;
        button3.GetComponent<Image>().color = shopMgr.features[2].renderer.color;
        button4.GetComponent<Image>().color = shopMgr.features[2].renderer.color;
        button5.GetComponent<Image>().color = shopMgr.features[2].renderer.color;

        InitialiseBtnlist();
    }

    void InitialiseBtnlist()
    {
        buttons = new List<Button>();
        hair.onClick.AddListener(() => shopMgr.SetCurrent(2));
        buttons.Add(hair);
        top.onClick.AddListener(() => shopMgr.SetCurrent(0));
        buttons.Add(top);
        bottom.onClick.AddListener(() => shopMgr.SetCurrent(1));
        buttons.Add(bottom);
    }

    public void UpdateChoice(int index)
    {
        shopMgr.SetChoice(index);
        Debug.Log("choice updated to = " + index);
        if (atShop)
        {
            shopMgr.features[shopMgr.currFeature].renderer.color = new Color(1, 1, 1);
        }
        else
        {
           // mgr.SetChoice(index);
            if (shopMgr.currFeature == 1)
            {
                shopMgr.features[shopMgr.currFeature].renderer.color = new Color(0, 0, 0, 1);
            }
        }
    }


    public void UpdateColorforHair(int index)
    {
        shopMgr.UpdateColor(index);
        button1.GetComponent<Image>().color = shopMgr.Getcolor(index);
        button2.GetComponent<Image>().color = shopMgr.Getcolor(index);
        button3.GetComponent<Image>().color = shopMgr.Getcolor(index);
        button4.GetComponent<Image>().color = shopMgr.Getcolor(index);
        button5.GetComponent<Image>().color = shopMgr.Getcolor(index);
    }

    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(buttons[shopMgr.currFeature].gameObject);
    }

    public void ToggleWardrobeBtn()
    {
        atShop = !atShop;
        if (atShop)
        {
            wardrobeBtn.image.sprite = wardrobeSprite;
            buttonText.text = "Wardrobe";
            //toggle.OpentopPanel();
            shopMgr.SetCurrent(0);
            //InitialiseBtnlist();
            UpdateShopOrWardrobe("shop_");
           
        }
        else
        {
            wardrobeBtn.image.sprite = shopSprite;
            buttonText.text = "Shop";
            //toggle.OpenWardrobeTopPanel();
            shopMgr.SetCurrent(0);
            for (int i = 0; i < shopMgr.features.Count; i++)
            {
                shopMgr.features[i].ID = shopMgr.features[i].ID.Replace("shop_", "");
               // shopMgr.features[i].SetFeatureID(id + shopMgr.features[i].ID);
                Debug.Log("ID " + shopMgr.features[i].ID);
            }
        }
    }

    public void Save()
    {
        shopMgr.SaveFeature();
    }

    public void UpdateShopOrWardrobe (string id)
    {
        for (int i = 0; i < shopMgr.features.Count; i++)
        {
            shopMgr.features[i].SetFeatureID(id + shopMgr.features[i].ID);
            Debug.Log("ID " + shopMgr.features[i].ID);
        }
    }

    public void SetIDforItems ()
    {
        generate.SetFeatureID(shopMgr.features[shopMgr.currFeature].ID);
        Debug.Log("Set to = " + shopMgr.currFeature);
    }

    public void setCurrentFeature (int current)
    {
        shopMgr.currFeature = current;
    }

    public void GoBackHouse()
    {
        SceneManager.LoadScene(1);
        //Debug.Log("count" + wardrobe.features.Count);
    }
}
