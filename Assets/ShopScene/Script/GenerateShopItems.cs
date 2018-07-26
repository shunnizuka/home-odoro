using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateShopItems : MonoBehaviour {

    private int CurrentFeature;
    private string FeatureId;

    public GameObject Shopitem;
    public GameObject Wardrobeitem;
    public GameObject panel;
    public GameObject colorPanel;
    public GameObject InsufficientHr;

    public List<GameObject> hairbtns;
    private GameObject oldpanel;

    public Sprite[] itemImages;

    public CharacterAppearance appearance;
    public PurchaseStatus status;
    public Data HoursAccumulated;

    // Use this for initialization
	void Start () {
        HoursAccumulated = FindObjectOfType<Data>();
	}
	
    public void SetFeatureID (string id)
    {
        FeatureId = id;
    }

	public void GenerateItems()
    {
        itemImages = Resources.LoadAll<Sprite>("Textures/" + FeatureId);
        Debug.Log(itemImages.Length);

        if (oldpanel != null) {
            Debug.Log("found item");
            Destroy(oldpanel);
            Debug.Log("destroyed item");
        }

        if (itemImages == null)
        {
            Debug.Log("no images");
        }

        //create the panel first
        GameObject newpanel = Instantiate(panel) as GameObject;
        newpanel.transform.SetParent(gameObject.transform.Find("Panel").transform , false);
        newpanel.SetActive(true);
        //for scroll to work
        gameObject.transform.Find("Panel").GetComponent<ScrollRect>().content = newpanel.GetComponent<RectTransform>();

        hairbtns = new List<GameObject>();
        //generate the shop items
        for (int i = 0; i < itemImages.Length; i++)
        {
    
            if (appearance.atShop && !status.CheckStatus(FeatureId, i))
            {
                int index = i;
                GameObject newitem = Instantiate(Shopitem) as GameObject;
                newitem.transform.Find("clotheBtn").GetComponent<Image>().sprite = itemImages[i];


                newitem.transform.Find("clotheBtn").GetComponent<Button>().onClick.AddListener(() => appearance.UpdateChoice(index));

                newitem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuyItem(status.GetPrice(FeatureId, index), index, newitem);
                });

                newitem.transform.Find("Text").GetComponent<Text>().text = status.GetPrice(FeatureId, index).ToString("0.00") + " Hr";
                newitem.transform.SetParent(newpanel.transform, false);

                //collate and initialise the color (only for hair button)
                if (FeatureId.Contains("hair"))
                {
                    newitem.transform.Find("clotheBtn").GetComponent<Image>().color = appearance.shopMgr.features[2].renderer.color;
                    hairbtns.Add(newitem);
                }
            }

            if (!appearance.atShop && status.CheckStatus(FeatureId, i))
            {
                int index = i;
                GameObject newitem = Instantiate(Wardrobeitem) as GameObject;
                newitem.transform.Find("clotheBtn").GetComponent<Image>().sprite = itemImages[i];
                newitem.transform.Find("clotheBtn").GetComponent<Button>().onClick.AddListener(() => appearance.UpdateChoice(index));

                newitem.transform.SetParent(newpanel.transform, false);

                //collate and initialise the color (only for hair button)
                if (FeatureId.Contains("hair"))
                {
                    newitem.transform.Find("clotheBtn").GetComponent<Image>().color = appearance.shopMgr.features[2].renderer.color;
                    hairbtns.Add(newitem);
                }
            }

        }
        Debug.Log("items created");
        oldpanel = newpanel;
    }

    public void BuyItem(float price, int index, GameObject item)
    {
        if (HoursAccumulated.SufficientHours(price))
        {
            Destroy(item);
            status.Purchased(FeatureId, index);
            HoursAccumulated.DeductHrs(price);
        }
        else
        {
            Debug.Log("Not enough hours");
            InsufficientHr.SetActive(true);
        }
    }

    public void CloseInsufficientHrPanel ()
    {
        InsufficientHr.SetActive(false);
    }

    public void OpenColourPanelHair()
    {
        colorPanel.SetActive(true);
        Debug.Log("color panel open");
        colorPanel.transform.SetAsLastSibling();
    }

    public void CloseColourPanel()
    {
        colorPanel.SetActive(false);
    }
}
