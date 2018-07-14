using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour
{
    public List<Feature> features;
    public int currFeature;

    //color options
    public Color black;
    public Color yellow;
    public Color brown;
    public Color red;
    public Color blue;
    public Color pink;
    public List<Color> colors;
    private int colorindex = 0;

    public FeatureManager(List<Feature> featurelist, List<Color> colorlist)
    {
        features = featurelist;
        colors = colorlist;
    }
       
    void InitialiseColor()
    {
        colors = new List<Color>();
        colors.Add(black);
        colors.Add(yellow);
        colors.Add(brown);
        colors.Add(red);
        colors.Add(blue);
        colors.Add(pink);
    }

    public void UpdateColor(int index2)
    {
        colorindex = index2;
        features[currFeature].SetColor(index2);
        features[currFeature].renderer.color = colors[index2];
    }

    public Color Getcolor(int index)
    {
        return colors[index];
    }

    private void OnEnable()
    {
        LoadFeature();
    }

    private void OnDisable()
    {
       SaveFeature();
    }

    private void OnApplicationQuit()
    {
        SaveFeature();
    }

    private void Start()
    {
        LoadFeature();
    }

    public void LoadFeature()
    {
        features = new List<Feature>();
        features.Add(new Feature("hair", transform.Find("hair").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("top", transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", transform.Find("bottom").GetComponent<SpriteRenderer>()));
        InitialiseColor();

       //features.Add(new Feature("shop_top", transform.Find("Top").GetComponent<SpriteRenderer>()));

        for (int i = 0; i< features.Count; i++)
        {
            string key = "FEATURE_" + i;
            string keycolor = "FEATURE_" + i + "color";
            features[i].currIndex = PlayerPrefs.GetInt(key);
            features[i].colorIndex = PlayerPrefs.GetInt(keycolor);
            features[i].UpdateFeature();
            features[i].renderer.color = colors[features[i].colorIndex];
        }
    }

    public void SaveFeature()
    {
        for (int i = 0; i < this.features.Count; i++)
        {
            string key = "FEATURE_" + i;
            string keycolor = "FEATURE_" + i + "color";
            PlayerPrefs.SetInt(key, features[i].currIndex);
            PlayerPrefs.SetInt(keycolor, features[i].colorIndex);
        }
        PlayerPrefs.Save();
    }

    public void LoadFeaturepublic()
    {
        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            string keycolor = "FEATURE_" + i + "color";
            features[i].currIndex = PlayerPrefs.GetInt(key);
            features[i].colorIndex = PlayerPrefs.GetInt(keycolor);
            features[i].UpdateFeature();
            features[i].renderer.color = colors[features[i].colorIndex];
        }
    }

    public void SetCurrent(int index)
    {
        if (features == null)
        {
            return;
        }
        currFeature = index;
    }

    public void NextChoice()
    {
        if (features == null)
            return;

        Debug.Log("Maxchoice = " + features[currFeature].Maxchoice());
        if (features[currFeature].currIndex >= features[currFeature].Maxchoice())
        {
            Debug.Log("exceeded choice");
            features[currFeature].currIndex = 0;
            features[currFeature].UpdateFeature();
        }
        else
        {
            features[currFeature].currIndex++;
            features[currFeature].UpdateFeature();
        }
    }

    public void PreviousChoice ()
    {
        if (features == null)
            return;

        if (features[currFeature].currIndex <= 0)
        {
            features[currFeature].currIndex = features[currFeature].Maxchoice();
            features[currFeature].UpdateFeature();
        }
        else
        {
            features[currFeature].currIndex--;
            features[currFeature].UpdateFeature();
        }
    }

    public void SetChoice(int index1)
    {
        if (features == null)
            return;
        features[currFeature].currIndex = index1;
        features[currFeature].UpdateFeature();
    }
}

[System.Serializable]
public class Feature
{
    public string ID;
    public int currIndex;
    public Sprite[] choices;
    public SpriteRenderer renderer;
    public int colorIndex;

    public void SetFeatureID (string id)
    {
        ID = id;
    }
    public Feature(string id, SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
       // UpdateFeature();
    }

    public void UpdateFeature ()
    {
        choices = Resources.LoadAll<Sprite>("Textures/" + ID);
        Debug.Log("ID =" + ID);

        if(choices == null)
        {
            Debug.Log("no choices");
        }

        if(choices == null || renderer == null)
        {
            return;
        }

        Debug.Log("currIndex = " + currIndex);
        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;
        
        renderer.sprite = choices[currIndex];
    }

   public int Maxchoice()
    {
        if (ID.Contains("top"))
            return 0;
        if (ID.Contains("bottom"))
            return 1;
        if (ID.Contains("hair"))
            return 4;
        return 0;
    }

    public void SetColor(int index)
    {
        colorIndex = index;
    }
}
