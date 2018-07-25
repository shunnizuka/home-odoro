using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FurniturePurchaseStatus : MonoBehaviour {

    private HouseFurnitureStatus houseStatus;
    public string path;
    string jsonString;

	// Use this for initialization
	void Start () {
        path = Application.persistentDataPath + "/HouseFurnitureStatus.json";
        if (System.IO.File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
        }
        else
        {
            TextAsset data = Resources.Load("HouseFurnitureStatus") as TextAsset;
            jsonString = data.ToString();
            Debug.Log("exists");
        }
        houseStatus = JsonUtility.FromJson<HouseFurnitureStatus>(jsonString);
    }

    public void Save()
    {
        string newhousestatus = JsonUtility.ToJson(houseStatus);
        File.WriteAllText(path, newhousestatus);
        Debug.Log(newhousestatus);
    }

    public void Purchased(string Id, int index)
    {
        if (Id.Contains("floor"))
            houseStatus.floor.furniturelist[index].bought = true;
        if (Id.Contains("Wall"))
            houseStatus.wall.furniturelist[index].bought = true;
        if (Id.Contains("desk"))
            houseStatus.desk.furniturelist[index].bought = true;
        if(Id.Contains("bed"))
            houseStatus.bed.furniturelist[index].bought = true;
        Save();
    }

    public bool CheckStatus(string Id, int index)
    {
        if (Id.Contains("floor"))
           return houseStatus.floor.furniturelist[index].bought;
        if (Id.Contains("Wall"))
           return  houseStatus.wall.furniturelist[index].bought;
        if (Id.Contains("desk"))
           return houseStatus.desk.furniturelist[index].bought;
        if (Id.Contains("bed"))
            return houseStatus.bed.furniturelist[index].bought;
        return false;
    }
}

[System.Serializable]
public class HouseFurnitureStatus
{
    public FurnitureList floor;
    public FurnitureList wall;
    public FurnitureList desk;
    public FurnitureList bed;
}

[System.Serializable]
public class FurnitureList
{
    public List<Furniture> furniturelist;
}

[System.Serializable]
public class Furniture
{
    public bool bought;
    public float price;
}
