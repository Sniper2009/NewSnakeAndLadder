using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTurnManager : MonoBehaviour {

    public delegate void RetVoidArgVoid(int num);
 //   public static event RetVoidArgVoid OnPlayerDiceChange;
    public static event RetVoidArgVoid OnPlayerChange;
    [SerializeField] Text playerTurnText;

    [SerializeField] List<GameObject> players;
  //  public static int playerTurn;
  //  public static GameObject currentPlayer;
	// Use this for initialization
	void Start () {
        MoveOneTile.OnGamestateChanged += AdvanceTurn;
      //  playerTurn = 1;
      ////  currentPlayer = players[playerTurn];
        //OnPlayerChange(playerTurn);
  
        //SetTurn(playerTurn);
	}
	



    void AdvanceTurn(int x)
    {

        //playerTurn = (playerTurn + 1) % players.Count;


        //SetTurn(playerTurn);
    }

    void SetTurn(int playerNum)
    {

     //   playerTurnText.text = "Player " + (playerTurn + 1).ToString() ;
        //if(OnPlayerDiceChange!=null)
        //OnPlayerDiceChange(DiceSelect.playerDice);
        //foreach (var item in players)
        //{
        //    item.GetComponent<MoveOneTile>().enabled = false;
        //    item.GetComponent<PlayerDiceHolding>().enabled = false;

        //}
      //  currentPlayer = players[playerNum];
      
      //  OnPlayerChange(playerTurn);
        //players[playerNum].GetComponent<MoveOneTile>().enabled = true;
        //players[playerNum].GetComponent<PlayerDiceHolding>().enabled = true;
    }


    private void OnDestroy()
    {
        MoveOneTile.OnGamestateChanged -= AdvanceTurn;
    }
}
