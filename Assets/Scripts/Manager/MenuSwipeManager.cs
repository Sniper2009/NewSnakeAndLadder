﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwipeManager : MonoBehaviour {


    [SerializeField] GameObject mainMenuObject;
    [SerializeField] GameObject diceMenuObject;
    [SerializeField] GameObject shopMenuObject;
	// Use this for initialization
	void Start () {

        SwipeDetector.OnSwipe += CheckMenuMove;
        GoToMaineMenu();
	}

    public void GoToDiceMenu()
    {
        mainMenuObject.SetActive(false);
        diceMenuObject.SetActive(true);
        shopMenuObject.SetActive(false);
    }

    public void GoToMaineMenu()
    {
        mainMenuObject.SetActive(true);
        diceMenuObject.SetActive(false);
        shopMenuObject.SetActive(false);
    }
    public void GoToShopMenu()
    {
        mainMenuObject.SetActive(false);
        diceMenuObject.SetActive(false);
        shopMenuObject.SetActive(true);
    }

    void CheckMenuMove(SwipeData sData)
    {
        Debug.Log("swip ing");
        if(mainMenuObject.activeSelf)
        {
            if(sData.Direction==SwipeDirection.Right)
            {
                GoToShopMenu();
            }
            if(sData.Direction==SwipeDirection.Left)
            {
                GoToDiceMenu();
            }
        }


        else if(diceMenuObject.activeSelf)
        {

            if (sData.Direction == SwipeDirection.Right)
            {
                GoToMaineMenu();
            }
        }

        else
        {
            if (sData.Direction == SwipeDirection.Left)
            {
                GoToMaineMenu();
            }
           
        }

    }


    private void OnDestroy()
    {
        SwipeDetector.OnSwipe -= CheckMenuMove;
    }
}
