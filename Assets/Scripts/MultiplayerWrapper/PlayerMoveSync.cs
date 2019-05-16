using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using IngameDebugConsole;

public class PlayerMoveSync : NetworkBehaviour {

    public delegate void RetVoidArgInt (int num);

    public event RetVoidArgInt OnDiceRolled;
    public event RetVoidArgInt OnTalismanDiceRolled;

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnActivateDice;
    public static event RetVoidArgInt OnDeactivateDice;

    bool isSet = false;
    [SyncVar]public int playerID=0;

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
            if(isServer!=false)
            GetComponent<Image>().color = playerColor;

        }
    }

    public override void OnStartLocalPlayer()
    {

        if(isServer)
        {
                    CmdSetID(2);
                    playerID = 2;
                    CmdSetPlayerColor();
            GetComponent<Image>().color = Color.white;
                   GetComponent< PlayerTurnReactor>().OnCurrentPlayerChange += CheckLocalPlayerActivation;
                    PlayerTurnReactor.OnCurrentPlayerChangeStatic += CheckLocalPlayerActivation;
        }
        else
        {
            CmdSetID(1);
            playerID = 1;
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

    void CmdSendTalismanDice(int diceNum)
    {

    }


    [ClientRpc]
    void RpcGetDiceRoll(int diceNum)
    {
        if (isLocalPlayer)
            return;
        OnDiceRolled(diceNum);
    }



    private void OnDestroy()
    {
        DiceMechanism.OnTalismanDiceRolled -= SendTalismanDice;
        DiceMechanism.OnDiceRolled -= SendDiceRoll;
        GetComponent<PlayerTurnReactor>().OnCurrentPlayerChange -= CheckLocalPlayerActivation;
        PlayerTurnReactor.OnCurrentPlayerChangeStatic -= CheckLocalPlayerActivation;
    }
}
