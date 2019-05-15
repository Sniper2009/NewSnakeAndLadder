using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour {

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnPlayersCollided;
    public static event RetVoidArgInt OnPlayersDidntCollide;
    // Use this for initialization
    void Start () {
        MoveOneTile.OnGamestateChanged += checkCollision;
	}
	
	void checkCollision(int ID)
    {
        Debug.Log("check collision:  "+ PlayerTurnReactor.currentPlayer + " nnnn  "+ PlayerTurnReactor.otherPlayer);
        if(PlayerTurnReactor.currentPlayer.currentTileNumber==PlayerTurnReactor.otherPlayer.currentTileNumber)
        {
            OnPlayersCollided(ID);
        }

        else
        {
            OnPlayersDidntCollide(ID);
        }
    }


    private void OnDestroy()
    {
        MoveOneTile.OnGamestateChanged -= checkCollision;
    }
}
