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
        if(playerNum!=player)
        {
            OtherPlayerMoveEnded();
        }
    }
}
