using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerEventAnnounce : MonoBehaviour {


    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OtherPlayerMoveEnded;
	// Use this for initialization
	void Start () {
        MoveOneTile.OnGamestateChanged += RespondToPlayerMoveEnd;
	}

    [SerializeField] int playerNum;
	void RespondToPlayerMoveEnd(int player)
    {
        Debug.Log("Other move ended: " + player + "   " + playerNum);
        if(playerNum!=player && PlayerTurnReactor.hitOtherEffect==false && PlayerTurnReactor.talismanEffect==0)
        {
            OtherPlayerMoveEnded();
        }
    }
}
