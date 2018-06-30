using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterAppearance : MonoBehaviour {

    private bool atShop = true;

    //
    private FeatureManager shop;
    private FeatureManager wardrobe;

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

    private void Start()
    { 
       shop = FindObjectOfType<FeatureManager>();
       shop.features = new List<Feature>();
       GameObject character = GameObject.Find("Character");
       shop.features.Add(new Feature("shop_top", character.transform.Find("Top").GetComponent<SpriteRenderer>()));
       shop.features.Add(new Feature("shop_bottom", character.transform.Find("bottom").GetComponent<SpriteRenderer>()));

       wardrobe = FindObjectOfType<FeatureManager>();
       InitialiseBtnlist();
    }

    void InitialiseBtnlist()
    {
        buttons = new List<Button>();
        hair.onClick.AddListener(() => shop.SetCurrent(2));
        buttons.Add(hair);
        top.onClick.AddListener(() => shop.SetCurrent(0));
        buttons.Add(top);
        bottom.onClick.AddListener(() => shop.SetCurrent(1));
        buttons.Add(bottom);
    }

    public void UpdateChoice(int index)
    {
        shop.SetChoice(index);
        shop.features[shop.currFeature].renderer.color = new Color(1, 1, 1);
    }

    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(buttons[shop.currFeature].gameObject);
    }

    public void ToggleWardrobeBtn()
    {
        atShop = !atShop;
        if(atShop)
        {
            wardrobeBtn.image.sprite = wardrobeSprite;
            buttonText.text = "Wardrobe";
        } else
        {
            wardrobeBtn.image.sprite = shopSprite;
            buttonText.text = "Shop";
        }
    }
}
