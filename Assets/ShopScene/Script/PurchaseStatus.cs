using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PurchaseStatus : MonoBehaviour {

    private ShopItemStatus shop;
    public string path;
    string jsonString;

    void Start()
    {
        path = Application.persistentDataPath + "/ShopItemStatus.json";
      /*  if(System.IO.File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
        }
        else
        {*/
            TextAsset data = Resources.Load("ShopItemStatus") as TextAsset;
            jsonString = data.ToString();
            Debug.Log("exists");
        //} 
        shop = JsonUtility.FromJson<ShopItemStatus>(jsonString);
    }

    public void Load() 
    {
        Debug.Log(shop.bottom.inventory[0].price);
    }

    public void Save()
    {
        string newshop = JsonUtility.ToJson(shop);
        File.WriteAllText(path, newshop);
        Debug.Log(newshop);
    }

    public void Purchased(string Id, int index)
    {
        if(Id.Contains("hair"))
            shop.hair.inventory[index].bought = true;
        if (Id.Contains("bottom"))
            shop.bottom.inventory[index].bought = true;
        if (Id.Contains("top"))
            shop.top.inventory[index].bought = true;
        Save();
    }

    public bool CheckStatus(string Id, int index)
    {
        if (Id.Contains("hair"))
            return shop.hair.inventory[index].bought;
        if (Id.Contains("bottom"))
            return shop.bottom.inventory[index].bought;
        if (Id.Contains("top"))
            return shop.top.inventory[index].bought;
        return false;
    }

}

[System.Serializable]
public class ShopItemStatus
{
    public Inventory top;
    public Inventory bottom;
    public Inventory hair;
}

[System.Serializable]
public class Inventory
{
    public List<Item> inventory;
}

[System.Serializable]
public class Item
{
    public bool bought;
    public float price;
}
