using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HouseManager : MonoBehaviour {

    public List<HouseFurnitures> Furnitures;
    public int currFurniture;

    public Button desk;
    public Button bed;
    public Button planner;
    public Button chart;

    //for the data
    public HouseData house;
    public string path;
    private string jsonstring;

    // Use this for initialization
    void Start()
    {
        LoadHouse();
        LoadData();
        LoadHouseSetting();
        Debug.Log("house loaded");
    }
    public void LoadHouse()
    {
            Furnitures = new List<HouseFurnitures>();
            Furnitures.Add(new HouseFurnitures("floor", transform.Find("floor").GetComponent<SpriteRenderer>()));
            Furnitures.Add(new HouseFurnitures("Wall1", transform.Find("Wall1").GetComponent<SpriteRenderer>()));
            Furnitures.Add(new HouseFurnitures("Wall2", transform.Find("Wall2").GetComponent<SpriteRenderer>()));
            Furnitures.Add(new HouseFurnitures("desk", desk));
            Furnitures.Add(new HouseFurnitures("bed", bed));
        
    }

    public void LoadData () {
        path = Application.persistentDataPath + "/Housedata.json";
        Debug.Log(Application.persistentDataPath);
        if (System.IO.File.Exists(path))
        {
            jsonstring = File.ReadAllText(path);
            house = JsonUtility.FromJson<HouseData>(jsonstring);
            Debug.Log("House file exists" + jsonstring);
        }
        //if no file yet
        if(house.housedata.Count == 0)
        {
            house.housedata = new List<Furnituredata>();
            for ( int i = 0; i< Furnitures.Count; i++)
            {
                Furnituredata newfurniture = new Furnituredata(0);
                house.housedata.Add(newfurniture);
            }    
        }
    }

    public void LoadHouseSetting()
    {
        for(int i = 0; i < Furnitures.Count; i++)
        {
            Furnitures[i].currIndex = house.housedata[i].FurnitureIndex;
            Furnitures[i].UpdateFeature();
        }
    }

    public void SaveHouseData ()
    {
        string newhousedata = JsonUtility.ToJson(house);
        File.WriteAllText(path, newhousedata);
        Debug.Log(newhousedata);
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

[System.Serializable]
public class HouseData
{
    public List<Furnituredata> housedata;

    public void Setdata(int id, int index)
    {
        housedata[id].FurnitureIndex = index;
        if(id == 1)
        {
            housedata[2].FurnitureIndex = index;
        }
    }
}

[System.Serializable]
public class Furnituredata
{
    public int FurnitureIndex;

    public Furnituredata(int index)
    {
        FurnitureIndex = index;
    }
}

