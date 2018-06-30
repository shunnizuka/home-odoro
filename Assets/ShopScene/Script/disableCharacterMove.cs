using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableCharacterMove : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject character = GameObject.Find("Character");
        character.GetComponent<Rigidbody2D>().simulated = false;
        character.transform.position = new Vector3(-1.85f, -2.26f, 89.918f);
        Debug.Log("disabled");

	}
	
}
