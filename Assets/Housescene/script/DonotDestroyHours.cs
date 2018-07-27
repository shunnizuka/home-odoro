using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonotDestroyHours : MonoBehaviour {

    public static DonotDestroyHours hours;

    private void Awake()
    {
        if (hours == null)
        {
            DontDestroyOnLoad(gameObject);
            hours = this;
            Debug.Log(Camera.main);
            hours.GetComponent<Canvas>().worldCamera = Camera.main;
            Debug.Log("hours not destroyed");
        }
        else if (hours != this)
        {
            Destroy(gameObject);
            hours.GetComponent<Canvas>().worldCamera = Camera.main;
            Debug.Log("camera changed");
        }
    }
}
