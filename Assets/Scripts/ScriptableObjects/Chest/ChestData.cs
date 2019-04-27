using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Chest/ChestData")]
public class ChestData : ScriptableObject {

    //ID: is it's order in collection
    public int chestID;
   public Sprite chestImage;
   
    public DateTimeSaveable openDuration;
    public Prize prize;
    // public float timeToOpen;


    //private void OnEnable()
    //{
    //    chestImage = Resources.Load<Sprite>("Chest/Chest_" + chestID);
    //}
}
