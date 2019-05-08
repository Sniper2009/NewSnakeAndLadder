using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestMenuBehaviour : MonoBehaviour,IPointerDownHandler {

    public delegate void RetVoidArgChetSave(SaveableChest chest);
    public static event RetVoidArgChetSave OnChestChanged;
    public static event RetVoidArgChetSave OnChestAssigned;
    public event RetVoidArgChetSave OnChestAssignedSingle;
    public static event RetVoidArgChetSave OnChestRemove;
    public static event RetVoidArgChetSave OnChestUnlocked;
    public static event RetVoidArgChetSave OnChestBrowsedUnlocking;

    public static event RetVoidArgChetSave OnPrizeOpen;



    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnChestReady;
    public static event RetVoidArgVoid OnChestBuyGems;
    public static event RetVoidArgVoid OnChestDisablePointer;
    public static event RetVoidArgVoid OnChestCheckPointer;
    public static event RetVoidArgVoid InOpeningChestBought;
   

    public delegate void RetVoidArgBool(bool b);
    public static event RetVoidArgBool OnChestClicked;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnChargeGem;


    [SerializeField] ChestCollection chestCollection;
    [SerializeField] GameObject chestPointerObject;

    bool chestClickedOn;
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
        ChestPopupUI.OnUnlockClicked += OnUnlockClicked;
        chestClickedOn = false;
        GetComponent<ChessMenuUI>().OnChestStateChanged += ChangeChestState;
        OnChestDisablePointer += DisablePointer;
        OnChestCheckPointer += CheckEnablePointer;
        ChestMenuPopop.OnPopupClosing += DisablePointer;
        ChestPopupUI.OnGemClicked += OnBuyWithGems;
    }

    public void AssignValues(SaveableChest saveableChest)
    {
      
        thisChest = saveableChest;
        chestID = saveableChest.chestID;
      if(saveableChest.chestState==ChestState.InOpening)
        {

            OnChestBrowsedUnlocking(thisChest);
        }
        if (chestReady==false)
            chestState = saveableChest.chestState;
      //  prize = saveableChest.prize;
        openDuration = saveableChest.openDurationSaveable;
        openOrderTime = saveableChest.openOrderTimeSaveable;
        Debug.Log("chest dur: " + saveableChest.openDuration+"   "+saveableChest.chestState);
        remainingTimeToOpen =System.DateTime.Now-saveableChest.openOrderTimeInSystem;
        GetComponent<ChessMenuUI>().EnrollInEvent();
        OnChestAssignedSingle(saveableChest);

    }

    void DisablePointer()
    {
        chestClickedOn = false;
        CheckEnablePointer();
    }
    void ChangeChestState(SaveableChest saveableChest)
    {
        chestReady = true;
      //  Debug.Log("in vhange:  "+saveableChest.chestState);
        thisChest.chestState = saveableChest.chestState;
        chestState = saveableChest.chestState;
        if(chestState==ChestState.InOpening)
        {
            OnChestUnlocked(thisChest);
        }
        if(chestState==ChestState.Ready)
        {
            OnChestReady();
        }
    }

    void CheckEnablePointer()
    {
        if(chestClickedOn)
        {
            chestPointerObject.SetActive(true);
        }

        else
        {
            chestPointerObject.SetActive(false);
        }
    }


    public void OnUnlockClicked()
    {
        if (chestClickedOn)
        {
            Debug.Log("chest unlock: ");
            thisChest.chestState = ChestState.InOpening;
            chestState = ChestState.InOpening;
            thisChest.openOrderTimeInSystem = System.DateTime.Now;
            SaveableChest newChest = new SaveableChest(chestID, chestState, System.DateTime.Now, openDuration, thisChest.chestType);
            OnChestUnlocked(newChest);
            OnChestChanged(newChest);
            OnChestAssigned(newChest);
            OnChestAssignedSingle(newChest);
        }
    }

    public void OnBuyWithGems()
    {
        if (chestClickedOn)
        {
            if (PlayerPrefs.GetInt("Gem") > chestCollection.chestCollection[thisChest.chestID].gemToOpen)//TODO change fixed
            {
                if(thisChest.chestState==ChestState.InOpening)
                {
                    Debug.Log("gem bbbbbbbbbb");
                    InOpeningChestBought();
                }
                OnChargeGem(-chestCollection.chestCollection[thisChest.chestID].gemToOpen);
                thisChest.chestState = ChestState.Ready;
                chestState = ChestState.Ready;
                OnChestAssignedSingle(thisChest);
                OnChestBuyGems();
                
            }
        }
    }





    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("chest: " + chestState);
        //if (chestState == ChestState.Closed )
        //GoToNextState();
        OnChestDisablePointer();
        chestClickedOn = true;
        OnChestClicked(chestState == ChestState.InOpening);
        OnChestCheckPointer();
        CheckEnablePointer();  
        if(chestState==ChestState.Ready)
        {
            Debug.Log("prize opened");
            OnPrizeOpen(thisChest);
            OnChestRemove(thisChest);
            Destroy(gameObject);
        }
        else
        {
            OnChestAssigned(thisChest);
        }
    }


    void OnDestroy()
    {
        ChestPopupUI.OnGemClicked -= OnBuyWithGems;
        ChestPopupUI.OnUnlockClicked -= OnUnlockClicked;
        OnChestDisablePointer -= DisablePointer;
        OnChestCheckPointer -= CheckEnablePointer;
        GetComponent<ChessMenuUI>().OnChestStateChanged -= ChangeChestState;
        ChestMenuPopop.OnPopupClosing -= DisablePointer;
    }
}
