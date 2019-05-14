using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEndMoveAction : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public event RetVoidArgVoid OnEndMoveDone;

    [SerializeField]float timeOfTravel = 2; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float normalizedValue;
    RectTransform rectTransform;
    MoveOneTile thisPlayerMove;
    MoveBackwards thisPlayerBack;
    // Use this for initialization
    void Start () {
        thisPlayerBack = GetComponent<MoveBackwards>();
        rectTransform = GetComponent<RectTransform>();
        thisPlayerMove = GetComponent<MoveOneTile>();
        thisPlayerMove.OnEndMoveEvent += ReactToEndMove;
        thisPlayerBack.OnEndMoveEvent += ReactToEndMove;
	}


    private void OnEnable()
    {

    }



    void ReactToEndMove(int endType)
    {
        TileInfoHolder currentTileNumber = thisPlayerMove.currentTileNumber;
        if (endType==0)
        {
            StartCoroutine(LerpToEnd(currentTileNumber.snakeTail.thisTilePos, currentTileNumber.snakeTail.thisTileMinAnchor, currentTileNumber.snakeTail.thisTileMaxAnchor));
        }
        if(endType==1)
        {
            StartCoroutine(LerpToEnd(currentTileNumber.ladderEnd.thisTilePos, currentTileNumber.ladderEnd.thisTileMinAnchor, currentTileNumber.ladderEnd.thisTileMaxAnchor));
        }
    }


    IEnumerator LerpToEnd(Vector3 endPosition,Vector2 minAnchor,Vector2 maxAnchor)
    {

        //rectTransform.anchorMin = minAnchor;
        //rectTransform.anchorMax = maxAnchor;

        while (currentTime <= timeOfTravel)
            {
                currentTime += Time.deltaTime;
                normalizedValue = currentTime / timeOfTravel; // we normalize our time 
            rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, maxAnchor, normalizedValue);
            rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, minAnchor, normalizedValue);
           
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, endPosition, normalizedValue);
                yield return null;
            }
            rectTransform.anchoredPosition = endPosition;
            currentTime = 0;
        
        OnEndMoveDone();

      
    }


    private void OnDestroy()
    {
        GetComponent<MoveOneTile>().OnEndMoveEvent -= ReactToEndMove;
        thisPlayerBack.OnEndMoveEvent -= ReactToEndMove;
    }
}
