using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnReactor : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnAIMove;
    public static event RetVoidArgVoid PlayerHit;
    public event RetVoidArgVoid OnMoveBackwards;
    public static event RetVoidArgInt OnMoveForward;
    public event RetVoidArgInt OnCurrentPlayerChange;
    public static event RetVoidArgInt OnCurrentPlayerChangeStatic;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnPlayerDiceChange;

    public delegate void RetVoidArg2Int(int i1, int i2);
    public static event RetVoidArg2Int OnGiveTakeCoin;

    MoveOneTile playermove;
    PlayerDiceHolding playerDice;
    PlayerMoveSync moveSync;
    MoveBackwards playerMoveBack;
    

    [SerializeField] int playerID;

    int currentID;
    static int talismanEffect = 0;
    static bool hitOtherEffect = false;
   
    public static MoveOneTile currentPlayer;
    public static MoveOneTile otherPlayer;
    public static int currentPlayerTurn;
    bool initialSet = false;

    private void Awake()
    {
       
        CheckPlayerCollision.OnPlayersDidntCollide += CheckForTurn;
        CheckPlayerCollision.OnPlayersCollided += CheckForOtherHit;
        playerDice = GetComponent<PlayerDiceHolding>();
        playermove = GetComponent<MoveOneTile>();
        moveSync = GetComponent<PlayerMoveSync>();
        playerMoveBack = GetComponent<MoveBackwards>();
        MoveBackwards.OnGamestateChanged += CheckAfterMoveBack;
        if (GetComponent<PlayerMoveSync>() != null)
            playerID = GetComponent<PlayerMoveSync>().playerID;
        else
        {
            DiceMechanism.OnTalismanDiceRolled += ActivateTalism;
            CheckForTurn(0);
        }

    }


    private void Update()
    {
        if(moveSync!=null && initialSet==false)
        {

            if (OnCurrentPlayerChange == null)
                return;
            playerID = GetComponent<PlayerMoveSync>().playerID;
            Debug.Log("got ID:  " + playerID);
            CheckForTurn(1);
            initialSet = true;
        }
    }
    // Use this for initialization
    void CheckForTurn(int ID)
    {
       
        if (talismanEffect>0)
        {
            talismanEffect --;
            if (playerID == 0 && OnAIMove != null)
                StartCoroutine(EventWithDelay());
            return;
        }
        playerMoveBack.enabled = false;
        if (ID == playerID)
        {
            Debug.Log("check turn Deactive:  " + ID + "    " + playerID);
            if (playerDice != null)
                playerDice.enabled = false;
            //if (moveSync != null)
                //moveSync.enabled = false;
            playermove.enabled = false;

            otherPlayer = playermove;

        }
        else
        {
            Debug.Log("check turn Active:  " + ID + "    " + playerID);
            if (playerDice != null)
                playerDice.enabled = true;
            if (moveSync != null)
                moveSync.enabled = true;
            playermove.enabled = true;
            currentPlayer = playermove;
            currentPlayerTurn = playerID;
            if (OnCurrentPlayerChange != null)
                OnCurrentPlayerChangeStatic(currentPlayerTurn);

            if (playerID == 0 &&moveSync==null&& OnAIMove != null)
                StartCoroutine(EventWithDelay());
        }


        //        Debug.Log("turn:  " + ID+"   "+name);

    }

    void ReactToTalisman(int diceNum)
    {
        ActivateTalism(diceNum, currentPlayer.playerNum);
    }

    void CheckAfterMoveBack(int ID)
    {
        Debug.Log("after come back");
        playerMoveBack.enabled = false;
        if (ID != playerID)
        {
            if (playerDice != null)
                playerDice.enabled = false;
            if (moveSync != null)
                moveSync.enabled = false;
            playermove.enabled = false;

            otherPlayer = playermove;

        }
        else
        {

            if (playerDice != null)
                playerDice.enabled = true;
            if (moveSync != null)
                moveSync.enabled = true;
            playermove.enabled = true;
            currentPlayer = playermove;
            currentPlayerTurn = playerID;
            //if (OnPlayerDiceChange != null)
                //OnPlayerDiceChange(DiceSelect.playerDice);
   
        }
    }



    void ActivateTalism(int diceNum,int ID)
    {

        SwapActivePlayer(true,ID);
        if (OnMoveForward != null && talismanEffect>0)
        {
            Debug.Log("in forwww");
            OnMoveForward(diceNum);
        }
        talismanEffect ++;
    }

    void CheckForOtherHit(int ID)
    {
        Debug.Log("in Hiiiiiit:  " + otherPlayer.playerNum + "    " + currentPlayer.playerNum);
       
            hitOtherEffect = true;
        if(playerID==ID)
        OnGiveTakeCoin(otherPlayer.playerNum, currentPlayer.playerNum);
            SwapActivePlayer(false,ID);
            if(OnMoveBackwards!=null)
            OnMoveBackwards();

    }

    void SwapActivePlayer(bool isMovingForward, int ID)
    {
        Debug.Log("in swaaaaap:  " + ID + "    " + playerID);
        if (ID == playerID)
        {
            if (playerDice != null)
                playerDice.enabled = false;
            if (moveSync != null)
                moveSync.enabled = false;
            playermove.enabled = false;
            playerMoveBack.enabled = false;
            otherPlayer = playermove;

        }
        else
        {
    
            if (playerDice != null)
                playerDice.enabled = true;
            if (moveSync != null)
                moveSync.enabled = true;
            playermove.enabled = isMovingForward;
            playerMoveBack.enabled = !isMovingForward;
            currentPlayer = playermove;
            currentPlayerTurn = playerID;
            if (OnCurrentPlayerChange != null)
                OnCurrentPlayerChange(currentPlayerTurn);

        }
    }


    IEnumerator EventWithDelay()
    {
        Debug.Log("start AI");
        yield return new WaitForSeconds(0.2f);
        OnAIMove();

    }

    private void OnDestroy()
    {
        if (GetComponent<PlayerMoveSync>() != null)
        {
           //register for events
        }
        else
        {
            DiceMechanism.OnTalismanDiceRolled -= ActivateTalism;
        }
        MoveBackwards.OnGamestateChanged -= CheckAfterMoveBack;
        // MoveOneTile.OnPlayerMoveEnded -= CheckForTurn;
        CheckPlayerCollision.OnPlayersDidntCollide -= CheckForTurn;
        CheckPlayerCollision.OnPlayersCollided -= CheckForOtherHit;
    }
}
