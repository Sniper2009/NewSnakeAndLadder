﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafePickup : MonoBehaviour {

    public delegate void RetVoidArgTwoIntTransform( int num2,Transform transform);
    public static event RetVoidArgTwoIntTransform OnRestoreSafe;

    public delegate void RetVoidArgIntBool(int num, bool state);
    public static event RetVoidArgIntBool OnSafeReplaced;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnSafeInteractionDone;
    public static event RetVoidArgInt OnSafePickupDone;


    [SerializeField] ChestCollection chestCollection;

    [SerializeField]List< Image> currentPlayerChest;
    [SerializeField] GameObject safeChoicePanel;
   // public static int[] playersSafeType = { 0, 0 };
    public static int[]playerSafeID ={0,0,0};
    int safeTile;
	// Use this for initialization
	void Start () {
      //  PlayerTurnReactor.OnPlayerDiceChange += DisplaySafeUpdate;
        MoveOneTile.OnEncounteredChest += OnSafePickupClicked;
        MoveBackwards.OnEncounteredChest += OnSafePickupClicked;
	}
	
	public void DisplaySafeUpdate(int playerTurn)
    {
        Debug.Log("in display update:  " + playerTurn + "    " + playerSafeID);
        //if (PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>() == null)
        //return;
        if (playerSafeID[playerTurn] > 0)
        {
            currentPlayerChest[playerTurn].enabled = true;
            currentPlayerChest[playerTurn].sprite = chestCollection.chestCollection[playerSafeID[playerTurn]].chestImage;
        }
    }



    public void UpdatePlayerSafe(int safeID)
    {
        
        playerSafeID[PlayerTurnReactor.currentPlayerTurn] = safeID;
        
       // playersSafeType[GameTurnManager.playerTurn] = safeNum;
    }


    public void OnSafePickupClicked(int tileNum)
    {

        //if (PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>() == null)
            //return;
        bool safeReplace=false;
        //restorePrevious one
       // int safeType = chestCollection.chestCollection[playersSafeType[GameTurnManager.playerTurn]].safeType;
        int safeID = chestCollection.chestCollection[playerSafeID[PlayerTurnReactor.currentPlayerTurn]].chestID;
        Debug.Log("restore:  " + PlayerTurnReactor.currentPlayer.playerNum);
        OnRestoreSafe(safeID, PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.transform);
       // Debug.Log("tilenum:  " + tileNum+"   "+safeID+"   "+safeType);
        //add new one
        if(safeID>0 )
        {
            safeReplace = true;
            Debug.Log("caammeee");
           PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.GetComponent<TileInfoHolder>().hasSafe = true;
        }
        //  safeType = GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.safeType;
        Debug.Log("current tile: " + PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.safeID+"    "+PlayerTurnReactor.currentPlayerTurn);
        safeID = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.safeID;
      //  OnRestoreSafe(safeType, safeID,);
        UpdatePlayerSafe(safeID);
        DisplaySafeUpdate(PlayerTurnReactor.currentPlayerTurn);
        OnSafePickupDone(tileNum);
        OnSafeReplaced(tileNum, safeReplace);
        StartCoroutine(ReturnWithDelay(tileNum));

    }


    IEnumerator ReturnWithDelay(int tileNum)
    {
        yield return new WaitForSeconds(2);
        OnSafeInteractionDone(tileNum);
    }


    private void OnDestroy()
    {
        PlayerTurnReactor.OnPlayerDiceChange -= DisplaySafeUpdate;
        MoveOneTile.OnEncounteredChest -= OnSafePickupClicked;
        MoveBackwards.OnEncounteredChest -= OnSafePickupClicked;
    }
}
