using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    WaitingForDice,
    PlayerMoving,
    WaitingForOther
}

public class GameStatusUIManager : MonoBehaviour {
    [SerializeField] GameObject Dice;
    [SerializeField] Text stateText;
    public GameState gameState;

	// Use this for initialization
	void Start () {
        MoveOneTile.OnGamestateChanged += RespondToGameStateChange;
        //GameTurnManager.OnPlayerChange+=
        gameState = GameState.WaitingForDice;
        RespondToGameStateChange(gameState);
	}
	

    

    void RespondToGameStateChange(GameState newState)
    {
        if(newState==GameState.WaitingForDice)
        {
            Dice.SetActive(true);
            stateText.text = "Roll the dice";
        }

        if (newState==GameState.PlayerMoving||newState==GameState.WaitingForOther)
        {
            Dice.SetActive(false);
            stateText.text = "Player is Moving";
        }
        gameState = newState;
    }

    private void OnDestroy()
    {
        MoveOneTile.OnGamestateChanged -= RespondToGameStateChange;
    }
}
