using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour {

    private static float totalhours = 0 ;
    public Text totaltime;

    private void Start()
    {
        GameObject.Find("Character").GetComponent<Rigidbody2D>().simulated = true;
    }

    public void Updatehour (float time) {
        Debug.Log("time before adding =" + totalhours);
        totalhours += time;
        Debug.Log("total hours = " + totalhours);
	}

    //to change the total hours accumulated displayed 
    public void DisplayTotaltimeText()
    {
        totaltime.GetComponentInChildren<Text>().text = totalhours +" hour";
        Debug.Log("display total time " + totaltime);
    }
}
