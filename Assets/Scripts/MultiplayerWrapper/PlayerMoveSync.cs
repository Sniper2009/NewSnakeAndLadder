using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using IngameDebugConsole;

public class PlayerMoveSync : NetworkBehaviour {

    public delegate void RetVoidArgInt (int num);

    public event RetVoidArgInt OnDiceRolled;

    public delegate void RetVoidArg2Int(int i1, int i2);
    public static event RetVoidArg2Int OnTalismanDiceRolled;

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnActivateDice;
    public static event RetVoidArgInt OnDeactivateDice;

    bool isSet = false;
    [SyncVar]public int playerID=0;
    public static int localPlayerID;

    [SyncVar] 
    Color playerColor;

    private void Start()
    {

        DiceMechanism.OnTalismanDiceRolled += SendTalismanDice;
        DiceMechanism.OnDiceRolled += SendDiceRoll;

        var temp = GameObject.Find("MultipleBoardTiles");
        transform.SetParent(temp.transform, false);
    }

    private void Update()
    {
        if(isLocalPlayer==false && playerID>0 && isSet==false)
        {
            isSet = true;
            GetComponent<PlayerTurnReactor>().OnCurrentPlayerChange += CheckLocalPlayerActivation;
            PlayerTurnReactor.OnCurrentPlayerChangeStatic += CheckLocalPlayerActivation;
            if(isServer==false)
            GetComponent<Image>().color = Color.white;

        }
    }

    public override void OnStartLocalPlayer()
    {

        if(isServer)
        {
                    CmdSetID(2);
                    playerID = 2;
            localPlayerID = 2;
                    CmdSetPlayerColor();
            GetComponent<Image>().color = Color.white;
                   GetComponent< PlayerTurnReactor>().OnCurrentPlayerChange += CheckLocalPlayerActivation;
                    PlayerTurnReactor.OnCurrentPlayerChangeStatic += CheckLocalPlayerActivation;
        }
        else
        {
            CmdSetID(1);
            playerID = 1;
            localPlayerID = 1;
            GetComponent<PlayerTurnReactor>().OnCurrentPlayerChange += CheckLocalPlayerActivation;
            PlayerTurnReactor.OnCurrentPlayerChangeStatic += CheckLocalPlayerActivation;
        }
    }




    [Command]
    void CmdSetPlayerColor()
    {
        playerColor = Color.white;
    }

    [Command]
    void CmdSetID(int ID)
    {
        Debug.Log("send command ID:  " + playerID);
        playerID = ID;
    }



    void CheckLocalPlayerActivation(int pNum)
    {
        if (isLocalPlayer)
        {
            Debug.Log("currentplayer: " + pNum + " this player: " + playerID);
            if (pNum == playerID)
            {
                OnActivateDice();
            }

            else
            {
                OnDeactivateDice(0);
            }
        }
    }


    void SendTalismanDice(int diceNum,int currentPlayer)
    {
        if(isLocalPlayer)
        CmdSendTalismanDice(diceNum,PlayerTurnReactor.currentPlayerTurn);
    }

    void SendDiceRoll(int diceNum)
    {
        if (isLocalPlayer)
            CmdSendDiceRoll(diceNum);
    }

    [Command]

    void CmdSendDiceRoll(int diceNum)
    {

        RpcGetDiceRoll(diceNum);
    }


    [Command]

    void CmdSendTalismanDice(int diceNum, int currentPlayer)
    {
        RpcSendTalisman(diceNum, currentPlayer);//the local player for talisman is disconnected in multiplayer
    }

    [ClientRpc]
    void RpcSendTalisman(int diceNum, int currentPlayer)
    {
     
        OnTalismanDiceRolled(diceNum, currentPlayer);
    }

    [ClientRpc]
    void RpcGetDiceRoll(int diceNum)
    {
        if (isLocalPlayer)
            return;
        OnDiceRolled(diceNum);//the local player is directly connected to dice event
    }



    private void OnDestroy()
    {
        DiceMechanism.OnTalismanDiceRolled -= SendTalismanDice;
        DiceMechanism.OnDiceRolled -= SendDiceRoll;
        GetComponent<PlayerTurnReactor>().OnCurrentPlayerChange -= CheckLocalPlayerActivation;
        PlayerTurnReactor.OnCurrentPlayerChangeStatic -= CheckLocalPlayerActivation;
    }
}
