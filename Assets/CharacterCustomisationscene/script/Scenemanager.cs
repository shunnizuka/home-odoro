using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour {

    public GameObject character;

	public void GotoHouse()
    {
        SceneManager.LoadScene(1);
        character.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void GotoShop( )
    {
        SceneManager.LoadScene(2);
    }
}
