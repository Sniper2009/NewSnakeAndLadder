﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessMenuUI : MonoBehaviour {

    public delegate void RetVoidArgChestsave(SaveableChest chest);
    public static event RetVoidArgChestsave OnChestChange;
    public event RetVoidArgChestsave OnChestStateChanged;

    [SerializeField] ChestCollection chestCollection;

    int childNum = 3;
    SaveableChest chest;
    System.TimeSpan remainingTime;
	// Use this for initialization
	void Awake () {

        GetComponent<ChestMenuBehaviour>().OnChestAssigned += DisplayChestInfo;
       

       // DisplayChestInfo();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (chest == null)
            return;
		if(chest.chestState==ChestState.InOpening)
        {
            remainingTime = (chest.openOrderTimeInSystem +chest.openDuration)- System.DateTime.Now;
        //    Debug.Log("time: " + chest.openOrderTimeInSystem+ "   " + chest.openDuration + "   " + System.DateTime.Now);
            transform.GetChild(1).GetChild(0).GetComponent<Text>().text = remainingTime.Hours.ToString() + ":" + remainingTime.Minutes.ToString()
                 + ":" + remainingTime.Seconds.ToString();

            if(remainingTime.Hours<=0 &&remainingTime.Minutes<=0 &&remainingTime.Seconds<=0)
            {
                Debug.Log("is ready");
                chest.chestState = ChestState.Ready;
                DisplayChestInfo(chest);
                OnChestChange(chest);
                OnChestStateChanged(chest);
            }
        }
    }


    void DisplayChestInfo(SaveableChest chestS)
    {
        GetComponent<Image>().sprite = chestCollection.chestCollection[chestS.chestID].chestImage;
        for (int i = 0; i < childNum; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        chest = chestS;
        Debug.Log("time: " + chest.openOrderTimeInSystem + "   " + chest.openDuration + "   " + System.DateTime.Now);
        if (chest.chestState == ChestState.Closed)
        {
            transform.GetChild(0).GetChild(1).GetComponent<Text>().text = chest.openDuration.Hours.ToString();
        }


        transform.GetChild((int)(chest.chestState)).gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        GetComponent<ChestMenuBehaviour>().OnChestAssigned -= DisplayChestInfo;
    }
}
