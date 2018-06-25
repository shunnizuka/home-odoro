using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontdestroyPlanner : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
