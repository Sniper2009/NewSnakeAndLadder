using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour {

    [SerializeField] Text currentPlayerCoin;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnPickedUpCoin;

    public static int[] playersCoin= { 0,0};
	// Use this for initialization
	void Start () {
        MoveOneTile.OnCameToTile += CheckforCoin;
        GameTurnManager.OnPlayerDiceChange += ChangeCoinUI;
	}
	
    void CheckforCoin(int dummy)
    {
//        Debug.Log("turn:  " + GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile);
        TileInfoHolder incomingTile = GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber;
        if (incomingTile.hasCoin)
        {
            playersCoin[GameTurnManager.playerTurn] += incomingTile.coinAmount;
            OnPickedUpCoin(incomingTile.tileNum);
            currentPlayerCoin.text = playersCoin[GameTurnManager.playerTurn].ToString();
        }
    }


    void ChangeCoinUI(int dummy)
    {
        currentPlayerCoin.text = playersCoin[GameTurnManager.playerTurn].ToString();
    }



    private void OnDestroy()
    {
        MoveOneTile.OnCameToTile -= CheckforCoin;
        GameTurnManager.OnPlayerDiceChange -= ChangeCoinUI;
    }
}
