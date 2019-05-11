using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMoveAlert : MonoBehaviour {
    [SerializeField]Text endMoveAlertText;
    GameObject[] players;
	// Use this for initialization
	void Start () {
        endMoveAlertText.text = "";
        MoveOneTile.OnGamestateChanged += ResetAlert;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            item.GetComponent<MoveOneTile>().OnEndMoveEvent += DisplayAlert;
        }
    }

    void DisplayAlert(int endType)
    {
        if(endType==0)
        {
            endMoveAlertText.text = "Player Hit Snake";
        }

        if(endType==1)
        {
            endMoveAlertText.text = "Player climbed ladder";
        }
    }


    void ResetAlert(int x)
    {
        endMoveAlertText.text = "";
    }


    private void OnDestroy()
    {
        MoveOneTile.OnGamestateChanged -= ResetAlert;
        foreach (var item in players)
        {
            item.GetComponent<MoveOneTile>().OnEndMoveEvent -= DisplayAlert;
        }
    }
}
