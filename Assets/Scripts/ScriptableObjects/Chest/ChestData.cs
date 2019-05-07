using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum ChestType
{

    wood,
     bronze,
     silver,
     gold,
     diamond
}

[CreateAssetMenu(menuName = "Chest/ChestData")]
public class ChestData : ScriptableObject {

    //ID: is it's order in collection
    public int chestID;
    public ChestType chestType;
   public Sprite chestImage;
   
    public DateTimeSaveable openDuration;
    public Prize prize;
    // public float timeToOpen;


    //private void OnEnable()
    //{
    //    chestImage = Resources.Load<Sprite>("Chest/Chest_" + chestID);
    //}
}
