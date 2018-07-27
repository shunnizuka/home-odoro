using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Data : MonoBehaviour {

    public Text totaltime;

    public HourData timeData;
    public string path;
    private string jsonString;

    private void Start()
    {
        GameObject.Find("Character").GetComponent<Rigidbody2D>().simulated = true;
        LoadData();
        DisplayTotaltimeText();
    }

    private void LoadData() 
    {
        path = Application.persistentDataPath + "/HourData.json";
        if (System.IO.File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
            timeData = JsonUtility.FromJson<HourData>(jsonString);
            Debug.Log("Hour file exists" + jsonString);
        }
        else
            timeData = new HourData(3);
    }

    public void Save()
    {
        string newTimeData = JsonUtility.ToJson(timeData);
        File.WriteAllText(path, newTimeData);
        Debug.Log(newTimeData);

    }
    public void Updatehour (float time) {
        Debug.Log("time before adding =" + timeData.totalhr.ToString());
        timeData.totalhr += time;
        Debug.Log("total hours = " + timeData.totalhr);
        Save();
	}

    //to change the total hours accumulated displayed 
    public void DisplayTotaltimeText()
    {
        GameObject.Find("hours").transform.Find("hoursAccumulated").transform.Find("Text").GetComponentInChildren<Text>().text = timeData.totalhr.ToString("0.000") +" hour";
        Debug.Log("display total time " + timeData.totalhr);
    }

    public bool SufficientHours(float price)
    {
        if (timeData.totalhr >= price)
            return true;
        return false;
    }

    public void DeductHrs(float hr)
    {
        timeData.totalhr -= hr;
        DisplayTotaltimeText();
        Save();
    }

}

public class HourData
{
    public float totalhr;

    public HourData (float hr)
    {
        totalhr = hr;
    }
}
