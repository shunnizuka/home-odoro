using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CharacterInfo : MonoBehaviour {

    public static CharacterInfo charinfo;

    public GameObject hair;
    public GameObject top;
    public GameObject bottom;

    private void Awake()
    {
        if(charinfo == null)
        {
            DontDestroyOnLoad(gameObject);
            charinfo = this;
        }
        else if(charinfo != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playInfo.dat");

        CharacterData data = new CharacterData();
       /* data.hair = hair.GetComponent<SpriteRenderer>();
        data.top = top.GetComponent<SpriteRenderer>();
        data.bottom = bottom.GetComponent<SpriteRenderer>();
        */

        data.top = top.GetComponent<SpriteRenderer>().sprite;
        Debug.Log("sprite serialised");
      //  data.topcolor = top.GetComponent<SpriteRenderer>().color;
        bf.Serialize(file, data);
        Debug.Log("color serialised");
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.Open);
            CharacterData data = (CharacterData)bf.Deserialize(file);
            file.Close();
           
            SpriteRenderer toprenderer = top.GetComponent<SpriteRenderer>();
         //   toprenderer = data.top;

           // toprenderer.color = data.topcolor;
            toprenderer.sprite = data.top;

            SpriteRenderer bottomrenderer = bottom.GetComponent<SpriteRenderer>();
            //bottomrenderer = data.bottom;
            SpriteRenderer hairrenderer = hair.GetComponent<SpriteRenderer>();
            //hairrenderer = data.hair;
        }
    }
}

[System.Serializable]
class CharacterData
{
    /* [SerializeField] public SpriteRenderer hair;
     [SerializeField] public SpriteRenderer top;
     [SerializeField] public SpriteRenderer bottom;
     */

    [SerializeField] public Sprite top;
   // public Color topcolor;
}
