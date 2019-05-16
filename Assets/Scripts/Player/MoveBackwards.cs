using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwards : MonoBehaviour {

    public delegate void RetVoidArgGamestate(GameState state);


    public delegate void RetVoidArgInt(int state);
    public event RetVoidArgInt OnEndMoveEvent;
    public static event RetVoidArgInt OnCameToTile;
    public static event RetVoidArgInt OnEncounteredChest;
    public event RetVoidArgInt OnPlayerMoveEnded;

    public static event RetVoidArgInt OnGamestateChanged;


    [SerializeField] float timeOfTravel = 2; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float normalizedValue;

    int playerNum;
    RectTransform rectTransform;
    bool isMoving = false;

    public TileInfoHolder currentTileNumber;

    private void Start()
    {
        playerNum = GetComponent<MoveOneTile>().playerNum;
        //GetComponent<PlayerTurnReactor>().OnMoveBackwards += MakeMove;
        //GetComponent<PlayerEndMoveAction>().OnEndMoveDone += EndPlayerMove;
        rectTransform = GetComponent<RectTransform>();
        //   DiceMechanism.OnDiceRolled += MakeMove;
    }

    private void OnEnable()
    {
        GetComponent<PlayerTurnReactor>().OnMoveBackwards += MakeMove;
        GetComponent<PlayerEndMoveAction>().OnEndMoveDone += EndPlayerMove;
        SafePickup.OnSafeInteractionDone += EndMoveAfterInteract;
    }

    void MakeMove()
    {

        currentTileNumber = GetComponent<MoveOneTile>().currentTileNumber;
        //  OnGamestateChanged(0);
        StartCoroutine(LerpObject(5));

    }




    IEnumerator LerpObject(int moveTimes)
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < moveTimes; i++)
        {

        
                while (currentTime <= timeOfTravel)
                {
                    currentTime += Time.deltaTime;
                    normalizedValue = currentTime / timeOfTravel; // we normalize our time 
              
                    rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, currentTileNumber.prevTile.thisTileMinAnchor, normalizedValue);
                    rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, currentTileNumber.prevTile.thisTileMaxAnchor, normalizedValue);
                    rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, currentTileNumber.prevTile.thisTilePos, normalizedValue);
                    yield return null;
                }
                rectTransform.anchoredPosition = currentTileNumber.prevTile.thisTilePos;
                currentTime = 0;
                currentTileNumber = currentTileNumber.prevTile;

        }
        // if(playerNum>0)
        GetComponent<MoveOneTile>().currentTileNumber = currentTileNumber;
        OnCameToTile(0);
        if (currentTileNumber.hasSafe)
        {
            //  if (playerNum > 0)
            checkTileForSafe();
            //else
            //CheckHouseForNonemptiness();
        }
        else
            CheckHouseForNonemptiness();

    }


    void checkTileForSafe()
    {
          
        OnEncounteredChest(currentTileNumber.tileNum);


    }




    void CheckHouseForNonemptiness()
    {
        if (currentTileNumber.isSnakeHead)
        {

            OnEndMoveEvent(0);
            currentTileNumber = currentTileNumber.snakeTail;
        }

        else if (currentTileNumber.isLadderHead)
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
        Debug.Log("check for safe return:   " + playerNum);
        OnGamestateChanged(playerNum);
    }
    void EndPlayerMove()
    {
        //Debug.Log("ending: " + 0);
        //if (playerNum > 0)
        //{
            if (currentTileNumber.hasSafe || currentTileNumber.hasCoin)
            {
                OnCameToTile(0);
                if (currentTileNumber.hasSafe)
                    checkTileForSafe();

            }
            else
            {
                OnGamestateChanged(playerNum);
                //    OnPlayerMoveEnded(playerNum);
            }
    
    }





    private void OnDisable()
    {
        SafePickup.OnSafeInteractionDone -= EndMoveAfterInteract;
        GetComponent<PlayerTurnReactor>().OnMoveBackwards -= MakeMove;
        GetComponent<PlayerEndMoveAction>().OnEndMoveDone -= EndPlayerMove;
    }
    private void OnDestroy()
    {
        GetComponent<PlayerEndMoveAction>().OnEndMoveDone -= EndPlayerMove;
        SafePickup.OnSafeInteractionDone -= EndMoveAfterInteract;
     
        GetComponent<PlayerTurnReactor>().OnMoveBackwards -= MakeMove;
    }
}
