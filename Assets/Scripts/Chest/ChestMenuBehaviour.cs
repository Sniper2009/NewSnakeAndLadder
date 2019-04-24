using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestMenuBehaviour : MonoBehaviour,IPointerDownHandler {

    public delegate void RetVoidArgChetSave(SaveableChest chest);
    public static event RetVoidArgChetSave OnChestChanged;
    public event RetVoidArgChetSave OnChestAssigned;
    public static event RetVoidArgChetSave OnChestRemove;

    public delegate void RetVoidArgPrize(Prize p);
    public static event RetVoidArgPrize OnPrizeOpen;

    [SerializeField] ChestCollection chestCollection;
    int chestID;
    bool chestReady = false;
    System.TimeSpan remainingTimeToOpen;
    DateTimeSaveable openDuration;
    DateTimeSaveable openOrderTime;
    ChestState chestState;
    Prize prize;
    int childNum = 3;
    SaveableChest thisChest;

    void Awake()
    {
        GetComponent<ChessMenuUI>().OnChestStateChanged += ChangeChestState;
    }

    public void AssignValues(SaveableChest saveableChest)
    {
        Debug.Log("assiggnnn");
        thisChest = saveableChest;
        chestID = saveableChest.chestID;
        if(chestReady==false)
        chestState = saveableChest.chestState;
        prize = saveableChest.prize;
        openDuration = saveableChest.openDurationSaveable;
        openOrderTime = saveableChest.openOrderTimeSaveable;
        Debug.Log("chest dur: " + saveableChest.openDuration);
        remainingTimeToOpen =System.DateTime.Now-saveableChest.openOrderTimeInSystem;
        OnChestAssigned(saveableChest);

    }


    void ChangeChestState(SaveableChest saveableChest)
    {
        chestReady = true;
        Debug.Log("in vhange:  "+saveableChest.chestState);
        thisChest.chestState = saveableChest.chestState;
        chestState = saveableChest.chestState;
    }


    void GoToNextState()
    {
        Debug.Log("nexxxttt");
        chestState = (ChestState)((int)chestState + 1);
        SaveableChest newChest = new SaveableChest(chestID, chestState, System.DateTime.Now, openDuration, prize);
        thisChest = newChest;
        OnChestChanged(newChest);
        OnChestAssigned(newChest);
    
    }

  

   

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("chest: " + chestState);
        if (chestState == ChestState.Closed )
            GoToNextState();

        if(chestState==ChestState.Ready)
        {
            Debug.Log("prize opened");
            OnPrizeOpen(prize);
            OnChestRemove(thisChest);
            Destroy(gameObject);
        }
    }


    void OnDestroy()
    {
        GetComponent<ChessMenuUI>().OnChestStateChanged -= ChangeChestState;
    }
}
