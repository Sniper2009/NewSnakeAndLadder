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
      //  PlayerTurnReactor.OnPlayerDiceChange += ChangeCoinUI;
	}
	
    void CheckforCoin(int dummy)
    {
//        Debug.Log("turn:  " + GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile);
        TileInfoHolder incomingTile = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber;
        if (incomingTile.hasCoin)
        {
            playersCoin[PlayerTurnReactor.currentPlayerTurn] += incomingTile.coinAmount;
            OnPickedUpCoin(incomingTile.tileNum);
            currentPlayerCoin[PlayerTurnReactor.currentPlayerTurn].text = playersCoin[PlayerTurnReactor.currentPlayerTurn].ToString();
        }
    }


    //void ChangeCoinUI(int dummy)
    //{
    //    currentPlayerCoin.text = playersCoin[PlayerTurnReactor.currentPlayerTurn].ToString();
    //}



    private void OnDestroy()
    {
        MoveOneTile.OnCameToTile -= CheckforCoin;
      //  PlayerTurnReactor.OnPlayerDiceChange -= ChangeCoinUI;
    }
}
