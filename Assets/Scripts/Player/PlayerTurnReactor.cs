using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnReactor : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnAIMove;
    MoveOneTile playermove;
    PlayerDiceHolding playerDice;
    

    [SerializeField] int playerID;



    private void Awake()
    {
      //  MoveOneTile.OnPlayerMoveEnded += CheckForTurn;
        GameTurnManager.OnPlayerChange += CheckForTurn;
        playerDice = GetComponent<PlayerDiceHolding>();
        playermove = GetComponent<MoveOneTile>();
    }
    // Use this for initialization
    void CheckForTurn(int ID)
    {
      
        if(ID!=playerID)
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
            if (ID == 0)
                StartCoroutine(EventWithDelay());
        }
//        Debug.Log("turn:  " + ID+"   "+name);
      
    }


    IEnumerator EventWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        OnAIMove();

    }

    private void OnDestroy()
    {
       // MoveOneTile.OnPlayerMoveEnded -= CheckForTurn;
        GameTurnManager.OnPlayerChange -= CheckForTurn;
    }
}
