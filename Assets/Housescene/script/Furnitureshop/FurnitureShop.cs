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
    public GameObject Inventoryitem;
    public GameObject panel;
    private GameObject oldpanel;

    //toggle shop/inventory 
    public Button shopbtn;
    public Text buttontext;
    public Sprite shop;
    public Sprite inventory;

    public GameObject insufficientHr;

    //furniture data
    public FurniturePurchaseStatus furnitureStatus;

    //number of hours
    public Data HoursAccumulated;

	// Use this for initialization
	void Start () {
        houseMgr = FindObjectOfType<HouseManager>();
        HoursAccumulated = FindObjectOfType<Data>();
	}
	
    public void UpdateFurnitureChoice(int index)
    {
        houseMgr.SetFurnitureChoice(index);
        //to save data when not at shop
        if(!atShop)
        {
            houseMgr.house.Setdata(houseMgr.currFurniture, houseMgr.Furnitures[houseMgr.currFurniture].currIndex);
            Debug.Log("house saved " + houseMgr.Furnitures[houseMgr.currFurniture].ID + houseMgr.Furnitures[houseMgr.currFurniture].currIndex);
        }
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
            //to generate shopitems that are not bought
            if (atShop && !furnitureStatus.CheckStatus(houseMgr.Furnitures[houseMgr.currFurniture].ID, i)) {
                int index = i;
                GameObject newitem = Instantiate(Shopitem) as GameObject;
                newitem.transform.Find("furniturebtn").GetComponent<Image>().sprite = items[i];
                newitem.transform.Find("furniturebtn").GetComponent<Button>().onClick.AddListener(() => UpdateFurnitureChoice(index));
                newitem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuyItem(furnitureStatus.GetPrice(houseMgr.Furnitures[houseMgr.currFurniture].ID, index), index, newitem);
                });
                newitem.transform.Find("price").GetComponent<Text>().text = furnitureStatus.GetPrice(houseMgr.Furnitures[houseMgr.currFurniture].ID, index).ToString("0.00") + " Hr";

                newitem.transform.SetParent(newpanel.transform, false);
            }

            if (!atShop && furnitureStatus.CheckStatus(houseMgr.Furnitures[houseMgr.currFurniture].ID, i)) {
                int index = i;
                GameObject newitem = Instantiate(Inventoryitem) as GameObject;
                newitem.transform.Find("furniturebtn").GetComponent<Image>().sprite = items[i];
                newitem.transform.Find("furniturebtn").GetComponent<Button>().onClick.AddListener(() => UpdateFurnitureChoice(index));

                newitem.transform.SetParent(newpanel.transform, false);
            }

        }
        Debug.Log("items created");
        oldpanel = newpanel;
    }

    public void BuyItem(float price, int index, GameObject item)
    {
        if (HoursAccumulated.SufficientHours(price))
        {
            furnitureStatus.Purchased(houseMgr.Furnitures[houseMgr.currFurniture].ID, index);
            houseMgr.house.Setdata(houseMgr.currFurniture, houseMgr.Furnitures[houseMgr.currFurniture].currIndex);
            HoursAccumulated.DeductHrs(price);
            Destroy(item);
        }
        else
        {
            Debug.Log("Not enough hours");
            insufficientHr.SetActive(true);
        }
    }

    public void ToggleInventoryBtn()
    {
        atShop = !atShop;
        if(atShop)
        {
            shopbtn.image.sprite = inventory;
            buttontext.text = "Inventory";
            GenerateFurnitureItems();
        }
        else
        {
            shopbtn.image.sprite = shop;
            buttontext.text = "Shop";
            GenerateFurnitureItems();
        }
    }
}
