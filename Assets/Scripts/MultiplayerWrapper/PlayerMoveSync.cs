using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMoveSync : NetworkBehaviour {

    public delegate void RetVoidArgInt (int num);

    public event RetVoidArgInt OnDiceRolled;
    public event RetVoidArgInt OnTalismanDiceRolled;

    public int playerID=0;

    private void Start()
    {
        DiceMechanism.OnTalismanDiceRolled += SendTalismanDice;
        DiceMechanism.OnDiceRolled += SendDiceRoll;
    }

    public override void OnStartLocalPlayer()
    {
        var temp = GameObject.Find("MultipleBoardTiles");
        transform.SetParent(temp.transform);
        if (isLocalPlayer)
            playerID = 1;  
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
    }
}
