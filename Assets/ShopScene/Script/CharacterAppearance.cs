using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterAppearance : MonoBehaviour
{
    public static bool atShop = true;

    public FeatureManager shopMgr;

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

    public TogglePanels toggle;
    public GenerateShopItems generate;

    private void Start()
    {
        shopMgr = FindObjectOfType<FeatureManager>();
        
        List<Feature> features = new List<Feature>();
        GameObject character = GameObject.Find("Character");
        features.Add(new Feature("top", character.transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", character.transform.Find("bottom").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("hair", character.transform.Find("hair").GetComponent<SpriteRenderer>()));
        shopMgr = new FeatureManager(features, shopMgr.colors);

        UpdateShopOrWardrobe("shop_");
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
        if (atShop && (shopMgr.currFeature != 2))
        {
            shopMgr.features[shopMgr.currFeature].renderer.color = new Color(1, 1, 1);
        }
        else
        {
            if (shopMgr.currFeature == 1)
            {
                shopMgr.features[shopMgr.currFeature].renderer.color = new Color(0, 0, 0, 1);
            }
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
        EventSystem.current.SetSelectedGameObject(buttons[shopMgr.currFeature].gameObject);
    }

    public void ToggleWardrobeBtn()
    {
        atShop = !atShop;
        if (atShop)
        {
            wardrobeBtn.image.sprite = wardrobeSprite;
            buttonText.text = "Wardrobe";
            UpdateShopOrWardrobe("shop_");
            generate.SetFeatureID(shopMgr.features[shopMgr.currFeature].ID);
            generate.GenerateItems();
            if (shopMgr.currFeature == 2)
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
            for (int i = 0; i < shopMgr.features.Count; i++)
            {
                shopMgr.features[i].ID = shopMgr.features[i].ID.Replace("shop_", "");
                Debug.Log("ID " + shopMgr.features[i].ID);
            }
            generate.SetFeatureID(shopMgr.features[shopMgr.currFeature].ID);
            generate.GenerateItems();
            if (shopMgr.currFeature == 2)
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
