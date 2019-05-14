using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameAnnounce : MonoBehaviour {
    [SerializeField] GameObject endGamePanel;
    Text endGameAnnounement;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnEndPanelActive;
   // public static event RetVoidArgVoid On

	// Use this for initialization
	void Start () {
        MoveOneTile.OnPlayerWon += EndGame;
        endGamePanel.SetActive(false);
        endGameAnnounement = endGamePanel.transform.GetChild(0).gameObject.GetComponent<Text>();
	}

    void EndGame(int playerNum)
    {
        endGameAnnounement.text = "Player " +( playerNum+1).ToString() + " won the game!!";
        endGamePanel.SetActive(true);
        OnEndPanelActive(playerNum);
    }

    public void OnRetryClicked()
    {
        SceneManager.LoadScene(0);
    }


    private void OnDestroy()
    {
        MoveOneTile.OnPlayerWon -= EndGame;
    }
}
