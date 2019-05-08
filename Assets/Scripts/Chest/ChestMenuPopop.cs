using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestMenuPopop : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnChestInOpening;
    public static event RetVoidArgVoid OnPopupClosing;

    public delegate void RetVoidArgSavechest(SaveableChest chest);
    public event RetVoidArgSavechest OnChestAssigned;
    bool noChestOpening;
    bool thisChestOpening;
    

    System.TimeSpan remainingTime;

    SaveableChest thisChest;

    [SerializeField] GameObject menuPopup;
    //[SerializeField] Image chestImage;
    [SerializeField] Text remainingTimeText;
    [SerializeField] Text gemToPay;
    [SerializeField] Text hourText;
  
    [SerializeField] GameObject GemOpen;
    [SerializeField] GameObject initialFirstOpen;
    [SerializeField] ChestCollection chestCollection;

    [SerializeField] List<string> persianPhrases;
    // Use this for initialization
    void Awake () {
        noChestOpening = true;

        menuPopup.SetActive(false);
        ChestMenuBehaviour.OnChestClicked += ThisChetopeningUpdate;
        ChestMenuBehaviour.OnChestAssigned += AssignChest;
       ChestMenuBehaviour.OnChestUnlocked += UnlockAttempt;
        ChestMenuPopop.OnChestInOpening += CheckChestStatus;
        ChestMenuBehaviour.OnChestReady += AChestReady;
        ChestMenuBehaviour.OnChestBuyGems += OnExitClicked;
        ChestMenuBehaviour.OnChestBrowsedUnlocking += OneChestUnlocking;
        ChessMenuUI.OnStateChangedVoid += OneChestFinishedLock;
        ChestMenuBehaviour.InOpeningChestBought += OneChestFinishedLock;


    }


    void AssignChest(SaveableChest chest)
    {
        Debug.Log("assignneeddd");
        thisChest = chest;
//        OnChestAssigned(thisChest);

        if (chest.chestState == ChestState.InOpening)
        {
            thisChestOpening = true;
            noChestOpening = false;
          //  OnChestInOpening();

        }
        OnChestAssigned(thisChest);
        ChestClickedOn();
    }
    
    void DisableAll()
    {
        remainingTimeText.enabled = false;
        GemOpen.SetActive(false);
        initialFirstOpen.SetActive(false);
    }
    void ChestClickedOn()
    {
        menuPopup.SetActive(true);
        DisableAll();

        Debug.Log("is chest open?: " + noChestOpening + "  " + thisChestOpening);
        if(noChestOpening)
        {

            ActivateInitialUnlock();
        }

        else
        {
            if(thisChestOpening)
            {
                ActivateThisChestInOpening();
            }

            else
            {
                ActvateOtherChestInOpening();
            }
        }
    }


    void ThisChetopeningUpdate(bool val)
    {
        thisChestOpening = val;
    }

    void UnlockAttempt(SaveableChest chest)
    {
        thisChest = chest;
        thisChest.chestState = ChestState.InOpening;
            thisChestOpening = true;
       
          

        noChestOpening = false;
        OnChestInOpening();
        ChestClickedOn();
    }


    void OneChestUnlocking(SaveableChest chest)
    {
        thisChest = chest;
        thisChest.chestState = ChestState.InOpening;
        thisChestOpening = true;



        noChestOpening = false;
        OnChestInOpening();
        ChestClickedOn();
        OnExitClicked();
    }

    void CheckChestStatus()
    {
        if (thisChest.chestState == ChestState.InOpening)
            return;
        thisChestOpening = false;
        noChestOpening = false;

    }



    void ActivateThisChestInOpening()
    {
        GemOpen.SetActive(true);
        gemToPay.text = chestCollection.chestCollection[thisChest.chestID].gemToOpen.ToString();
       
        remainingTimeText.enabled = true;
        remainingTimeText.color = Color.green;
        remainingTime = (thisChest.openOrderTimeInSystem + thisChest.openDuration) - System.DateTime.Now;
        remainingTimeText.text = persianPhrases[5]+remainingTime.Hours+":"+remainingTime.Minutes+":"+remainingTime.Seconds;
    }

    void AChestReady()
    {
        noChestOpening = true;
        thisChestOpening = false;
    }


    private void FixedUpdate()
    {
        if (thisChestOpening == true)
        {
            remainingTime = (thisChest.openOrderTimeInSystem + thisChest.openDuration) - System.DateTime.Now;
            remainingTimeText.text = persianPhrases[5] + remainingTime.Hours + ":" + remainingTime.Minutes + ":" + remainingTime.Seconds;
            if (remainingTime.Hours <= 0 && remainingTime.Minutes <= 0 && remainingTime.Seconds <= 0)
            {

                OnExitClicked();
            }
        }
    }

    void OneChestFinishedLock()
    {
      
        if (menuPopup.activeSelf == false)
            return;
        if(thisChest.chestState!=ChestState.InOpening)
        {
            noChestOpening = true;
            thisChestOpening = false;
            ChestClickedOn();
        }
        else
        {
            noChestOpening = true;
            thisChestOpening = false;
        }
    }

  

    void ActvateOtherChestInOpening()
    {
        GemOpen.SetActive( true);
        gemToPay.text = chestCollection.chestCollection[thisChest.chestID].gemToOpen.ToString();

        remainingTimeText.enabled = true;
        remainingTimeText.color = Color.red;
        remainingTimeText.text = persianPhrases[6];
    }

    void ActivateInitialUnlock()
    { 

        initialFirstOpen.SetActive( true);
        hourText.text = thisChest.openDuration.Hours.ToString();
    }



    public void OnExitClicked()
    {
        OnPopupClosing();
        menuPopup.SetActive(false);
    }

    // Update is called once per frame
    void OnDestroy () {
        ChestMenuBehaviour.OnChestClicked -= ThisChetopeningUpdate;
        ChestMenuBehaviour.OnChestAssigned -= AssignChest;
        ChestMenuBehaviour.OnChestUnlocked -= UnlockAttempt;
        ChestMenuPopop.OnChestInOpening -= CheckChestStatus;
        ChestMenuBehaviour.OnChestReady -= AChestReady;
        ChestMenuBehaviour.OnChestBuyGems -= OnExitClicked;
        ChestMenuBehaviour.OnChestBrowsedUnlocking -= OneChestUnlocking;
        ChessMenuUI.OnStateChangedVoid -= OneChestFinishedLock;
        ChestMenuBehaviour.InOpeningChestBought -= OneChestFinishedLock;

    }

  
}
