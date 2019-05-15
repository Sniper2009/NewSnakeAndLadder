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
        LocalPlayerEventAnnounce.OtherPlayerMoveEnded += RespondToOtherPlayerDone;
        DiceMechanism.OnDiceRolled += RespondToDiceRole;
        PlayerMoveSync.OnActivateDice += RespondToOtherPlayerDone;
        PlayerMoveSync.OnDeactivateDice += RespondToDiceRole;
        //GameTurnManager.OnPlayerChange+=
        gameState = GameState.WaitingForDice;
        RespondToOtherPlayerDone();
	}
	

    

    void RespondToOtherPlayerDone()
    {
        Dice.SetActive(true);
    }

    void RespondToDiceRole(int dummy)
    {
        Dice.SetActive(false);
    }

    private void OnDestroy()
    {
        LocalPlayerEventAnnounce.OtherPlayerMoveEnded -= RespondToOtherPlayerDone;
        PlayerMoveSync.OnActivateDice -= RespondToOtherPlayerDone;
        PlayerMoveSync.OnDeactivateDice -= RespondToDiceRole;
    }
}
