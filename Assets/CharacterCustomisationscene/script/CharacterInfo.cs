using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CharacterInfo : MonoBehaviour
{

    public static CharacterInfo charinfo;

    public GameObject hair;
    public GameObject top;
    public GameObject bottom;

    private void Awake()
    {
        if (charinfo == null)
        {
            DontDestroyOnLoad(gameObject);
            charinfo = this;
        }
        else if (charinfo != this)
        {
            Destroy(gameObject);
        }
    }

}