using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo charinfo;
    public CharacterData character;
    public string path;
    private string jsonString;

    public GameObject hair;
    public GameObject top;
    public GameObject bottom;

    private void Awake()
    {
        if (charinfo == null)
        {
            DontDestroyOnLoad(gameObject);
            charinfo = this;
        }
        else if (charinfo != this)
        {
            Destroy(gameObject);
            Debug.Log("character destroyed");
        }
    }

    void Start()
    {
        path = Application.persistentDataPath + "/Characterdata.json";
        Debug.Log(Application.persistentDataPath);
        if (System.IO.File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
            character = JsonUtility.FromJson<CharacterData>(jsonString);
            Debug.Log("character file exists" + jsonString);
        }
    }

    public void Save()
    {
        path = Application.persistentDataPath + "/Characterdata.json";
        string newcharacter = JsonUtility.ToJson(character);
        File.WriteAllText(path, newcharacter);
        Debug.Log(newcharacter);
    }

    public void LoadFeatures()
    {
        if (character.characterdata.Count == 0)
        { 
            character.characterdata = new List<Clothes>();

            for ( int i = 0; i < 3; i++)
            {
                Clothes newclothes = new Clothes(0, 0);
                character.characterdata.Add(newclothes);
            }
        }
    }
}

[System.Serializable]
public class CharacterData
{
    //public Clothes[] characterdata = new Clothes[3];

    public List<Clothes> characterdata;

    public void SetHair(int hair, int hColor)
    {
        this.characterdata[0].FeatureIndex = hair;
        this.characterdata[0].ColorIndex = hColor;
    }

    public void SetTop(int top, int tColor)
    {
        this.characterdata[1].FeatureIndex = top;
        this.characterdata[1].ColorIndex = tColor;
    }


    public void SetBottom(int bottom, int bColor)
    {
        this.characterdata[2].FeatureIndex = bottom;
        this.characterdata[2].ColorIndex = bColor;
    }

    public override string ToString() {
        return "Character Data: " + characterdata;
    }

}

[System.Serializable]
public class Clothes
{
    public int FeatureIndex;
    public int ColorIndex;

    public Clothes (int feature, int color) {
        FeatureIndex = feature;
        ColorIndex = color;
    }
}