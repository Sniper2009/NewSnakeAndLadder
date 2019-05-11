using System.Collections;
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

    [SerializeField] Image currentPlayerChest;
    [SerializeField] GameObject safeChoicePanel;
   // public static int[] playersSafeType = { 0, 0 };
    public static int[]playerSafeID ={0,0};
    int safeTile;
	// Use this for initialization
	void Start () {
        PlayerTurnReactor.OnPlayerDiceChange += DisplaySafeUpdate;
        MoveOneTile.OnEncounteredChest += OnSafePickupClicked;
	}
	
	public void DisplaySafeUpdate(int playerTurn)
    {
        if (PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>() == null)
            return;
      
        currentPlayerChest.sprite = chestCollection.chestCollection[playerSafeID[0]].chestImage;
    }



    public void UpdatePlayerSafe(int safeID)
    {

        playerSafeID[0] = safeID;
       // playersSafeType[GameTurnManager.playerTurn] = safeNum;
    }


    public void OnSafePickupClicked(int tileNum)
    {

        if (PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>() == null)
            return;
        bool safeReplace=false;
        //restorePrevious one
       // int safeType = chestCollection.chestCollection[playersSafeType[GameTurnManager.playerTurn]].safeType;
        int safeID = chestCollection.chestCollection[playerSafeID[0]].chestID;

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
        Debug.Log("current tile: " + PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.safeID);
        safeID = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.safeID;
      //  OnRestoreSafe(safeType, safeID,);
        UpdatePlayerSafe(safeID);
        DisplaySafeUpdate(0);
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
    }
}
