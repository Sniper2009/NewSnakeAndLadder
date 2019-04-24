using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour {

   
    public static SaveLoadManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public bool IsSaveFile(string saveFolder)
    {
        return Directory.Exists(saveFolder);
    }
    public void SaveItems(string saveFolder,string saveItemFolder,string filePath,SaveableItem ItemsHeld)
    {
   
        if (!IsSaveFile(saveFolder))
            Directory.CreateDirectory(saveFolder);
    
        Directory.CreateDirectory(saveItemFolder);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        var json = JsonUtility.ToJson(ItemsHeld);
    
      //  Debug.Log("in format: " + json+"   "+dd.di);
        bf.Serialize(file, json);
    
        file.Close();
    }

    public void LoadItems(string filePath, SaveableItem toLoadData)
    {
        if(!File.Exists(filePath))
        {
            Debug.Log("load folder doesn't exist");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), toLoadData);
        file.Close();


    }

}
