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
            hours.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        else if (hours != this)
        {
            Destroy(gameObject);
            hours.GetComponent<Canvas>().worldCamera = Camera.main;
            Debug.Log("camera changed");
        }
    }
}
