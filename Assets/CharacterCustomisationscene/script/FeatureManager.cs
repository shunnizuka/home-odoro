using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour
{
    public List<Feature> features;
    public int currFeature;

    private void OnEnable()
    {
        LoadFeature();
    }

    private void OnDisable()
    {
        SaveFeature();
    }

    void LoadFeature()
    {
        features = new List<Feature>();
        features.Add(new Feature("hair", transform.Find("hair").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("top", transform.Find("Top").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("bottom", transform.Find("bottom").GetComponent<SpriteRenderer>()));

        for (int i = 0; i< features.Count; i++)
        {
            string key = "FEATURE_" + i;
            PlayerPrefs.SetInt(key, features[i].currIndex);
            features[i].currIndex = PlayerPrefs.GetInt(key);
            features[i].UpdateFeature();
        }
    }

    void SaveFeature()
    {
        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            PlayerPrefs.SetInt(key, features[i].currIndex);
        }
        PlayerPrefs.Save();
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
        features[currFeature].currIndex++;
        features[currFeature].UpdateFeature();
    }

    public void PreviousChoice ()
    {
        if (features == null)
            return;
        features[currFeature].currIndex--;
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
    private Color color;

    public Feature(string id, SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
        UpdateFeature();
    }

    public void UpdateFeature ()
    {
        choices = Resources.LoadAll<Sprite>("Textures/" + ID);

        if(choices == null)
        {
            Debug.Log("no choices");
        }

        if(choices == null || renderer == null)
        {
            return;
        }

        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;

        renderer.sprite = choices[currIndex];
    }

    public void SetColor(Color newcolor)
    {
        color = newcolor;
        renderer.color = color;
    }
 
}

