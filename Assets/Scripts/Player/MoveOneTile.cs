using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOneTile : MonoBehaviour {

    public delegate void RetVoidArgGamestate(GameState state);
    public static event RetVoidArgGamestate OnGamestateChanged;

    public delegate void RetVoidArgInt(int state);
    public event RetVoidArgInt OnEndMoveEvent;
    public static event RetVoidArgInt OnPlayerWon;
    public static event RetVoidArgInt OnCameToTile;
    public static event RetVoidArgInt OnEncounteredChest;
    public static event RetVoidArgInt OnPlayerMoveEnded;
    

    [SerializeField] int endTileNum;


    public int playerNum;
    [SerializeField]float timeOfTravel = 2; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float normalizedValue;
  
    RectTransform rectTransform;
    bool isMoving=false;

    public TileInfoHolder currentTileNumber;

    private void Start()
    {
        Debug.Log("start: ");
        GetComponent<PlayerEndMoveAction>().OnEndMoveDone += EndPlayerMove;
        rectTransform = GetComponent<RectTransform>();
     //   DiceMechanism.OnDiceRolled += MakeMove;
    }

    private void OnEnable()
    {
        DiceMechanism.OnDiceRolled += MakeMove;
        if(playerNum==0)
        RandomMove.OnMoveAI += MakeMove;
        SafePickup.OnSafeInteractionDone += EndMoveAfterInteract;
    }

    void MakeMove(int num)
    {
        OnGamestateChanged(GameState.PlayerMoving);
        StartCoroutine(LerpObject(num));

    }

  


    IEnumerator LerpObject( int moveTimes)
    {
        if(rectTransform==null)
            rectTransform = GetComponent<RectTransform>();
        bool keepMoving = true;
        for (int i = 0; i < moveTimes; i++)
        {

            if (currentTileNumber.tileNum == endTileNum && keepMoving==true)//last tile
            {
                OnPlayerWon(playerNum);
                keepMoving = false;
            }

            if (keepMoving == true)
            {
                while (currentTime <= timeOfTravel)
                {
                    currentTime += Time.deltaTime;
                    normalizedValue = currentTime / timeOfTravel; // we normalize our time 

                    rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, currentTileNumber.nextTile.thisTileMinAnchor, normalizedValue);
                    rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, currentTileNumber.nextTile.thisTileMaxAnchor, normalizedValue);
                    rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, currentTileNumber.nextTile.thisTilePos, normalizedValue);
                    yield return null;
                }
                rectTransform.anchoredPosition = currentTileNumber.nextTile.thisTilePos;
                currentTime = 0;
                currentTileNumber = currentTileNumber.nextTile;
            }
        }
        if(playerNum>0)
        OnCameToTile(0);
        if (currentTileNumber.hasSafe)
        {
            if (playerNum > 0)
                checkTileForSafe();
            else
                CheckHouseForNonemptiness();
        }
        else
            CheckHouseForNonemptiness();

    }


    void checkTileForSafe()
    {
//        Debug.Log("current: " + currentTileNumber.tileNum);
        OnEncounteredChest(currentTileNumber.tileNum);
      

    }


   

    void CheckHouseForNonemptiness()
    {
        if(currentTileNumber.isSnakeHead)
        {

            OnEndMoveEvent(0);
            currentTileNumber = currentTileNumber.snakeTail;
        }

        else if(currentTileNumber.isLadderHead)
        {
           
            OnEndMoveEvent(1);
            currentTileNumber = currentTileNumber.ladderEnd;
        }
        else
        {
            EndPlayerMove();
        }
    }

    void EndMoveAfterInteract(int dummy)
    {
        OnGamestateChanged(GameState.WaitingForDice);
    }
    void EndPlayerMove()
    {
        Debug.Log("ending: " + playerNum);
        if (playerNum > 0)
        {
            if (currentTileNumber.hasSafe || currentTileNumber.hasCoin)
            {
                OnCameToTile(0);
                if (currentTileNumber.hasSafe)
                    checkTileForSafe();

            }
            else
            {
                OnGamestateChanged(GameState.WaitingForDice);
            //    OnPlayerMoveEnded(playerNum);
            }
        }
        else
        {
            OnGamestateChanged(GameState.WaitingForDice);
//            OnPlayerMoveEnded(playerNum);
        }
    }





    private void OnDisable()
    {
        SafePickup.OnSafeInteractionDone -= EndMoveAfterInteract;
        DiceMechanism.OnDiceRolled -= MakeMove;
        RandomMove.OnMoveAI -= MakeMove;
    }
    private void OnDestroy()
    {
        GetComponent<PlayerEndMoveAction>().OnEndMoveDone -= EndPlayerMove;
        DiceMechanism.OnDiceRolled -= MakeMove;
        SafePickup.OnSafeInteractionDone -= EndMoveAfterInteract;
        RandomMove.OnMoveAI -= MakeMove;
    }
}
