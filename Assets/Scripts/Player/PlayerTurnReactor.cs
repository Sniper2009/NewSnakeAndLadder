using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnReactor : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnAIMove;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnPlayerDiceChange;

    MoveOneTile playermove;
    PlayerDiceHolding playerDice;
    

    [SerializeField] int playerID;

   
    public static MoveOneTile currentPlayer;
    public static int currentPlayerTurn;

    private void Awake()
    {
      //  MoveOneTile.OnPlayerMoveEnded += CheckForTurn;
        MoveOneTile.OnGamestateChanged += CheckForTurn;
        playerDice = GetComponent<PlayerDiceHolding>();
        playermove = GetComponent<MoveOneTile>();
        CheckForTurn(0);
    }
    // Use this for initialization
    void CheckForTurn(int ID)
    {
        Debug.Log("trn to check:  " + ID);
        if(ID==playerID)
        {
            if (playerDice != null)
                playerDice.enabled = false;
            playermove.enabled = false;
          
        }
        else
        {
            if (playerDice != null)
                playerDice.enabled = true;
            playermove.enabled = true;
            currentPlayer = GetComponent<MoveOneTile>();
            currentPlayerTurn = playerID;
            if(OnPlayerDiceChange!=null)
            OnPlayerDiceChange(DiceSelect.playerDice);
            if (playerID == 0)
                StartCoroutine(EventWithDelay());
        }
//        Debug.Log("turn:  " + ID+"   "+name);
      
    }


    IEnumerator EventWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        OnAIMove();

    }

    private void OnDestroy()
    {
        // MoveOneTile.OnPlayerMoveEnded -= CheckForTurn;
        MoveOneTile.OnGamestateChanged -= CheckForTurn;
    }
}
