using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour {

    [SerializeField]List< Text> currentPlayerCoin;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnPickedUpCoin;

    public static int[] playersCoin= { 0,0};
	// Use this for initialization
	void Start () {
        MoveOneTile.OnCameToTile += CheckforCoin;
        MoveBackwards.OnCameToTile += CheckforCoin;
        PlayerTurnReactor.OnGiveTakeCoin += GetCurrentCoins;
        //  PlayerTurnReactor.OnPlayerDiceChange += ChangeCoinUI;
    }
	
    void CheckforCoin(int dummy)
    {
        Debug.Log("turn:  " + PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile);
        TileInfoHolder incomingTile = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber;
        if (incomingTile.hasCoin)
        {
            playersCoin[PlayerTurnReactor.currentPlayerTurn] += incomingTile.coinAmount;
            OnPickedUpCoin(incomingTile.tileNum);
            currentPlayerCoin[PlayerTurnReactor.currentPlayerTurn].text = playersCoin[PlayerTurnReactor.currentPlayerTurn].ToString();
        }
    }


    void GetCurrentCoins(int givingID, int takingID)
    {
        Debug.Log(" before coin:  " + takingID + "   " + givingID);
        playersCoin[takingID] += playersCoin[givingID];

        playersCoin[givingID] = 0;
        Debug.Log(" after coin:  " + givingID + "   " + currentPlayerCoin[takingID].name+"   "+currentPlayerCoin[givingID].name);
        currentPlayerCoin[takingID].text = playersCoin[takingID].ToString();
        currentPlayerCoin[givingID].text = playersCoin[givingID].ToString();
    }


    //void ChangeCoinUI(int dummy)
    //{
    //    currentPlayerCoin.text = playersCoin[PlayerTurnReactor.currentPlayerTurn].ToString();
    //}



    private void OnDestroy()
    {
        MoveOneTile.OnCameToTile -= CheckforCoin;
        MoveBackwards.OnCameToTile -= CheckforCoin;
        PlayerTurnReactor.OnGiveTakeCoin -= GetCurrentCoins;
        //  PlayerTurnReactor.OnPlayerDiceChange -= ChangeCoinUI;
    }
}
