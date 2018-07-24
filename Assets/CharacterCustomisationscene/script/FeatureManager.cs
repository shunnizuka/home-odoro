using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour
{
    public List<Feature> features;
    public int currFeature;
    public CharacterInfo data;

    //color options
    public Color black;
    public Color yellow;
    public Color brown;
    public Color red;
    public Color blue;
    public Color pink;
    public List<Color> colors;
    private int colorindex = 0;

   /* public FeatureManager(List<Feature> featurelist, List<Color> colorlist)
    {
        features = featurelist;
        colors = colorlist;
    }
    */

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
        SetSaveSetting();
        features[currFeature].renderer.color = colors[index2];
    }

    public Color Getcolor(int index)
    {
        return colors[index];
    }

    private void OnEnable()
    {
       //LoadFeature();
       //LoadcharFeatures();
    }

    private void OnDisable()
    {
        // SaveFeature();
        data.Save();
        Debug.Log("character saved");
    }

    void Start()
    {
        LoadFeature();
        LoadcharFeatures();
        Debug.Log("game started");
    }

    public void LoadFeature()
    {
        features = new List<Feature>();
        features.Add(new Feature("hair", transform.Find("hair").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("top", transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", transform.Find("bottom").GetComponent<SpriteRenderer>()));
        InitialiseColor();
    }

    public void LoadcharFeatures()
    {
        data.LoadFeatures();
        Debug.Log(features.Count);
        for (int i = 0; i < features.Count; i++)
        {
            features[i].currIndex = data.character.characterdata[i].FeatureIndex;
            features[i].colorIndex = data.character.characterdata[i].ColorIndex;
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
        Debug.Log(currFeature);
    }

    public void SetSaveSetting()
    {
        switch (currFeature)
        {
            case 0:
                {
                    data.character.SetHair(features[currFeature].currIndex, features[0].colorIndex);
                    Debug.Log("hair saved = " + features[currFeature].currIndex);
                    break;
                }
            case 1:
                {
                    data.character.SetTop(features[currFeature].currIndex, features[currFeature].colorIndex);
                    Debug.Log("top saved = " + features[currFeature].currIndex);
                    break;
                }
            case 2:
                {
                    data.character.SetBottom(features[currFeature].currIndex, features[currFeature].colorIndex);
                    Debug.Log("bottom saved = " + features[currFeature].currIndex);
                    break;
                }
        }
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
            SetSaveSetting();
            features[currFeature].UpdateFeature();
        }
        else
        {
            features[currFeature].currIndex++;
            SetSaveSetting();
            features[currFeature].UpdateFeature();
        }
    }

    public void PreviousChoice()
    {
        if (features == null)
            return;

        if (features[currFeature].currIndex <= 0)
        {
            features[currFeature].currIndex = features[currFeature].Maxchoice();
            SetSaveSetting();
            features[currFeature].UpdateFeature();
        }
        else
        {
            features[currFeature].currIndex--;
            SetSaveSetting();
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

    public void SetFeatureID(string id)
    {
        ID = id;
    }
    public Feature(string id, SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
        // UpdateFeature();
    }

    public void UpdateFeature()
    {
        choices = Resources.LoadAll<Sprite>("Textures/" + ID);
        //Debug.Log("ID =" + ID);

        if (choices == null)
        {
            Debug.Log("no choices");
        }

        if (choices == null || renderer == null)
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