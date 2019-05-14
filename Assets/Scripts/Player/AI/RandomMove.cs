using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {


    public delegate void RetVoidArgInt(int move);
    public static event RetVoidArgInt OnMoveAI;
    MoveOneTile playerMove;
    int playerID = 0;
    private void Awake()
    {
        Debug.Log("awake randooooooooooooooooooom");
        PlayerTurnReactor.OnAIMove += move;
        playerMove = GetComponent<MoveOneTile>();

    }



    void move()
    {
        int index = Random.Range(9, 10);
        Debug.Log("random: " + index);
       OnMoveAI(index);
    }


    private void OnDestroy()
    {
        PlayerTurnReactor.OnAIMove -= move;
    }


}
