using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterAppearance : MonoBehaviour
{
    public bool atShop = true;

    public FeatureManager shopMgr;

    //navigation buttons
    private List<Button> buttons;
    public Button hair;
    public Button top;
    public Button bottom;

    //wardrobe/shop button
    public Sprite wardrobeSprite;
    public Sprite shopSprite;
    public Text buttonText;
    public Button wardrobeBtn;

    public GenerateShopItems generate;

    private void Start()
    {
        shopMgr = FindObjectOfType<FeatureManager>();
       /* 
        List<Feature> features = new List<Feature>();
        GameObject character = GameObject.Find("Character");
        features.Add(new Feature("top", character.transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", character.transform.Find("bottom").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("hair", character.transform.Find("hair").GetComponent<SpriteRenderer>()));
        shopMgr = new FeatureManager(features, shopMgr.colors);
        */
      //  InitialiseBtnlist();
    }

   /* void InitialiseBtnlist()
    {
        buttons = new List<Button>();
        hair.onClick.AddListener(() => shopMgr.SetCurrent(0));
        buttons.Add(hair);
        top.onClick.AddListener(() => shopMgr.SetCurrent(1));
        buttons.Add(top);
        bottom.onClick.AddListener(() => shopMgr.SetCurrent(2));
        buttons.Add(bottom);
    }
    */
    public void UpdateChoice(int index)
    {
        shopMgr.SetChoice(index);
        //save the changes made from trying on clothes in wardrobe but not shop
        if (!atShop)
            shopMgr.SetSaveSetting();
        Debug.Log("choice updated to = " + index);
        
        //set color to white, excluding the hair
        if (shopMgr.currFeature != 0)
        {
            shopMgr.features[shopMgr.currFeature].renderer.color = Color.white;
        }
    }


    public void UpdateColorforHair(int index)
    {
        shopMgr.UpdateColor(index);
        for (int i = 0; i < generate.hairbtns.Count; i++)
        {
            Debug.Log(generate.hairbtns.Count);
            generate.hairbtns[i].transform.Find("clotheBtn").GetComponent<Image>().color = shopMgr.Getcolor(index);
        }
}

    private void Update()
    {
        //EventSystem.current.SetSelectedGameObject(buttons[shopMgr.currFeature].gameObject);
    }

    public void ToggleWardrobeBtn()
    {
        atShop = !atShop;
        if (atShop)
        {
            wardrobeBtn.image.sprite = wardrobeSprite;
            buttonText.text = "Wardrobe";
            generate.SetFeatureID(shopMgr.features[shopMgr.currFeature].ID);
            generate.GenerateItems();
            if (shopMgr.currFeature == 0)
            {
                generate.OpenColourPanelHair();
                Debug.Log("called opencolor");
            }
            else
                generate.CloseColourPanel(); 
        }
        else
        {
            wardrobeBtn.image.sprite = shopSprite;
            buttonText.text = "Shop";
            generate.SetFeatureID(shopMgr.features[shopMgr.currFeature].ID);
            generate.GenerateItems();
            if (shopMgr.currFeature == 0)
            {
                generate.OpenColourPanelHair();
                Debug.Log("called opencolor");
            }
            else
                generate.CloseColourPanel();
        }
    }

    public void Save()
    {
      //  shopMgr.SaveFeature();
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
        shopMgr.LoadcharFeatures();
    }
}
