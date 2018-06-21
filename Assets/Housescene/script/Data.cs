using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

    private static float totalhours = 0 ;
	

	public void Updatehour (float time) {
        totalhours += time;
	}
}
