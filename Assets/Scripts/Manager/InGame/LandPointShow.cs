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
        DiceMechanism.OnDiceRolled += DiceRollHappened;
        LocalPlayerEventAnnounce.OtherPlayerMoveEnded += WaitingForDiceRoll;

        DiceSelector.OnDiceNumbers += DisplayPointers;
        if(PlayerTurnReactor.currentPlayer!=null)
        locationCurrentTile = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile;
        if(PlayerTurnReactor.currentPlayer!=null &&PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>()!=null)
        DisplayPointers(diceCollection.diceFullDesigns[PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>().currentDiceID].nums);
    }


    void WaitingForDiceRoll()
    {

            if(PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>() != null)
            DisplayPointers(diceCollection.diceFullDesigns[ PlayerTurnReactor.currentPlayer.GetComponent<PlayerDiceHolding>().currentDiceID].nums);
        
    }


    void DisplayPointers(List<int>diceNumbers)
    {
        List<int> diceNums = diceNumbers.OfType<int>().ToList();
        locationCurrentTile = PlayerTurnReactor.currentPlayer.GetComponent<MoveOneTile>().currentTileNumber.nextTile;
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


    void DiceRollHappened(int x)
    {

            foreach (var item in instantiatedPoints)
            {
                item.transform.SetParent(board, false);
                item.SetActive(false);
            }
        
    }


    private void OnDestroy()
    {
        DiceMechanism.OnDiceRolled += DiceRollHappened;
        LocalPlayerEventAnnounce.OtherPlayerMoveEnded += WaitingForDiceRoll;
        DiceSelector.OnDiceNumbers -= DisplayPointers;
    }
}
