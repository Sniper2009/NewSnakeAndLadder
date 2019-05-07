using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LandPointShow : MonoBehaviour {

    [SerializeField] Transform board;
    [SerializeField] DiceDesignCollection diceCollection;
    //[SerializeField] GameObject pointerObject;
   [SerializeField] List<GameObject> instantiatedPoints;
    int tileToShow = 8;
    bool initialShow = false;

    //private void Update()
    //{
    //    if(initialShow==false && locationCurrentTile.nextTile!=null)
    //    {
    //        for (int i = 0; i < tileToShow; i++)
    //        {
    //            Debug.Log("loc: " + locationCurrentTile.name + "   " + locationCurrentTile.nextTile);
    //            instantiatedPoints[i].transform.SetParent(locationCurrentTile.gameObject.transform, false);
    //            instantiatedPoints[i].SetActive(true);

    //            locationCurrentTile = locationCurrentTile.nextTile;
    //        }
    //        initialShow = true;
    //    }
    //}

    public TileInfoHolder locationCurrentTile;

    private void Start()
    {
        MoveOneTile.OnGamestateChanged += WaitingForDiceRoll;
        MoveOneTile.OnGamestateChanged += DiceRollHappened;

        DiceSelector.OnDiceNumbers += DisplayPointers;
        locationCurrentTile = GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile;
        if(GameTurnManager.currentPlayer.GetComponent<PlayerDiceHolding>()!=null)
        DisplayPointers(diceCollection.diceFullDesigns[GameTurnManager.currentPlayer.GetComponent<PlayerDiceHolding>().currentDiceID].nums);
    }


    void WaitingForDiceRoll(GameState state)
    {
        if (state == GameState.WaitingForDice)
        {
            if(GameTurnManager.currentPlayer.GetComponent<PlayerDiceHolding>() != null)
            DisplayPointers(diceCollection.diceFullDesigns[ GameTurnManager.currentPlayer.GetComponent<PlayerDiceHolding>().currentDiceID].nums);
        }
    }


    void DisplayPointers(List<int>diceNumbers)
    {
        List<int> diceNums = diceNumbers.OfType<int>().ToList();
        locationCurrentTile = GameTurnManager.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile;
        for (int i = 0; i < tileToShow; i++)
        {
            
//                Debug.Log("loc: " + diceNumbers.ToString());
            if (locationCurrentTile == null)
                return;
            if (diceNums.Contains(i+1))
                {
                instantiatedPoints[i].transform.SetParent(locationCurrentTile.gameObject.transform, false);
                instantiatedPoints[i].SetActive(true);
            }
            else
            {
                instantiatedPoints[i].SetActive(false);
            }

            locationCurrentTile = locationCurrentTile.nextTile;
        }
    }


    void DiceRollHappened(GameState state)
    {
        if (state == GameState.PlayerMoving)
        {
            foreach (var item in instantiatedPoints)
            {
                item.transform.SetParent(board, false);
                item.SetActive(false);
            }
        }
    }


    private void OnDestroy()
    {
        MoveOneTile.OnGamestateChanged -= WaitingForDiceRoll;
        MoveOneTile.OnGamestateChanged -= DiceRollHappened;
        DiceSelector.OnDiceNumbers -= DisplayPointers;
    }
}
