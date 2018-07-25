using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureShop : MonoBehaviour {

    public bool atShop = true;

    public HouseManager houseMgr;

    //to generate the shop items
    public Sprite[] items;
    public GameObject Shopitem;
    public GameObject panel;
    private GameObject oldpanel;

    //toggle shop/inventory 
    public Button shopbtn;
    public Text buttontext;
    public Sprite shop;
    public Sprite inventory;
    
	// Use this for initialization
	void Start () {
        houseMgr = FindObjectOfType<HouseManager>();
	}
	
    public void UpdateFurnitureChoice(int index)
    {
        houseMgr.SetFurnitureChoice(index);
        //to save data when not at shop
    }

    public void GenerateFurnitureItems()
    {
        items = Resources.LoadAll<Sprite>("Textures/" + houseMgr.Furnitures[houseMgr.currFurniture].ID);
        Debug.Log(houseMgr.Furnitures[houseMgr.currFurniture].ID + items.Length);

        if (oldpanel != null)
        {
            Debug.Log("found item");
            Destroy(oldpanel);
            Debug.Log("destroyed item");
        }

        if (items == null)
        {
            Debug.Log("no images");
        }

        GameObject newpanel = Instantiate(panel) as GameObject;
        newpanel.transform.SetParent(gameObject.transform.Find("ParentPanel").transform, false);
        newpanel.SetActive(true);
        gameObject.transform.Find("ParentPanel").GetComponent<ScrollRect>().content = newpanel.GetComponent<RectTransform>();

        for (int i = 0; i < items.Length; i++)
        {
            int index = i;
            GameObject newitem = Instantiate(Shopitem) as GameObject;
            newitem.transform.Find("furniturebtn").GetComponent<Image>().sprite = items[i];
            newitem.transform.Find("furniturebtn").GetComponent<Button>().onClick.AddListener(() => UpdateFurnitureChoice(index));
            newitem.transform.SetParent(newpanel.transform, false);
        }
        Debug.Log("items created");
        oldpanel = newpanel;
    }

    public void ToggleInventoryBtn()
    {
        atShop = !atShop;
        if(atShop)
        {
            shopbtn.image.sprite = inventory;
            buttontext.text = "Inventory";
        }
        else
        {
            shopbtn.image.sprite = shop;
            buttontext.text = "More Furniture";
        }
    }
}
