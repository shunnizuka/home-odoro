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

    private GameObject oldpanel;

    public Sprite[] itemImages;

    public CharacterAppearance appearance;

    // Use this for initialization
	void Start () {
		
	}
	
    public void SetFeatureID (string id)
    {
        FeatureId = id;
    }

	public void GenerateItems ()
    {
        itemImages = Resources.LoadAll<Sprite>("Textures/" + FeatureId);

        if (oldpanel != null) {
            Debug.Log("found item");
            Destroy(oldpanel);
            Debug.Log("destroyed item");
        }

        if (itemImages == null)
        {
            Debug.Log("no images");
        }

        GameObject newpanel = Instantiate(panel) as GameObject;
        newpanel.transform.SetParent(gameObject.transform, false);
        newpanel.SetActive(true);
        Debug.Log("panel created");

        for( int i = 0; i < itemImages.Length; i++)
        {
            GameObject newitem = Instantiate(Shopitem) as GameObject;
            newitem.transform.Find("clotheBtn").GetComponent<Image>().sprite = itemImages[i];
            int index = i;
            newitem.transform.Find("clotheBtn").GetComponent<Button>().onClick.AddListener(() => appearance.UpdateChoice(index));
            newitem.transform.SetParent(newpanel.transform, false);
        }
        Debug.Log("items created");
        oldpanel = newpanel;
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
