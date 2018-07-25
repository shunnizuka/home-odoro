using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour {

    public List<HouseFurnitures> Furnitures;
    public int currFurniture;

    public Button desk;
    public Button bed;
    public Button planner;
    public Button chart;

	// Use this for initialization
	void Start () {

        Furnitures = new List<HouseFurnitures>();
        Furnitures.Add(new HouseFurnitures("floor", transform.Find("floor").GetComponent<SpriteRenderer>()));
        Furnitures.Add(new HouseFurnitures("Wall1", transform.Find("Wall1").GetComponent<SpriteRenderer>()));
        Furnitures.Add(new HouseFurnitures("Wall2", transform.Find("Wall2").GetComponent<SpriteRenderer>()));
        Furnitures.Add(new HouseFurnitures("desk", desk));
        Furnitures.Add(new HouseFurnitures("bed", bed));
    }

    public void SetCurrentFurniture(int index)
    {
        if (Furnitures == null)
        {
            return;
        }
        currFurniture = index;
        Debug.Log(currFurniture);
    }

    public void SetFurnitureChoice(int index1)
    {
        if (Furnitures == null)
            return;
        Furnitures[currFurniture].currIndex = index1;
        if(currFurniture == 1)
        {
            Furnitures[2].currIndex = index1;
        }
        Furnitures[currFurniture].UpdateFeature();
        Furnitures[2].UpdateFeature();
    }

}

public class HouseFurnitures
{
    public string ID;
    public int currIndex;
    public Sprite[] choices;
    public SpriteRenderer renderer;
    public Button furniture;

    public HouseFurnitures(string id, SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
    }

    public HouseFurnitures(string id, Button btn)
    {
        ID = id;
        furniture = btn;
    }

    public void UpdateFeature()
    {
        choices = Resources.LoadAll<Sprite>("Textures/" + ID);
        //Debug.Log("ID =" + ID);

        if (choices == null)
        {
            Debug.Log("no choices");
        }

        Debug.Log("currIndex = " + currIndex);
        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;

        if (renderer != null)
            renderer.sprite = choices[currIndex];
        else
            furniture.image.sprite = choices[currIndex];
    }
}

