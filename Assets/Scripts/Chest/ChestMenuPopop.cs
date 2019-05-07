using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestMenuPopop : MonoBehaviour,IPointerDownHandler {

    public delegate void RetVoidArgSavechest();
    public static event RetVoidArgSavechest OnChestInOpening;

    bool noChestOpening;
    bool thisChestOpening;

    System.TimeSpan remainingTime;

    SaveableChest thisChest;

    [SerializeField] GameObject menuPopup;
    [SerializeField] Image chestImage;
    [SerializeField] Text remainingTimeText;
    [SerializeField] Text chestName;
    [SerializeField] Text minPrize;
    [SerializeField] GameObject GemOpen;
    [SerializeField] GameObject initialFirstOpen;
    [SerializeField] ChestCollection chestCollection;
	// Use this for initialization
	void Awake () {
        noChestOpening = true;

        menuPopup.SetActive(false);
        GetComponent<ChestMenuBehaviour>().OnChestAssigned += AssignChest;
       GetComponent<ChestMenuBehaviour>().OnChestUnlocked += UnlockAttempt;
        ChestMenuPopop.OnChestInOpening += CheckChestStatus;
        ChestMenuBehaviour.OnChestReady += AChestReady;

	}


    void AssignChest(SaveableChest chest)
    {
        Debug.Log("assignneeddd");
        thisChest = chest;
        chestImage.sprite = chestCollection.chestCollection[thisChest.chestID].chestImage;
        if (chest.chestState == ChestState.InOpening)
        {
            thisChestOpening = true;
            noChestOpening = false;
            OnChestInOpening();

        }
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

    void UnlockAttempt(SaveableChest chest)
    {

        thisChest.chestState = ChestState.InOpening;
            thisChestOpening = true;
       
          

        noChestOpening = false;
        OnChestInOpening();
        ChestClickedOn();
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
        GemOpen.transform.GetChild(0).GetComponent<Text>().text = "Gems";
        remainingTimeText.enabled = true;
        remainingTimeText.color = Color.green;
        remainingTime = (thisChest.openOrderTimeInSystem + thisChest.openDuration) - System.DateTime.Now;
        remainingTimeText.text = "remaining: "+remainingTime.Hours+":"+remainingTime.Minutes+":"+remainingTime.Seconds;
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
            remainingTimeText.text = "remaining: " + remainingTime.Hours + ":" + remainingTime.Minutes + ":" + remainingTime.Seconds;
        }
    }


    void ActvateOtherChestInOpening()
    {
        GemOpen.SetActive( true);
        GemOpen.transform.GetChild(0).GetComponent<Text>().text = "Gems";
        remainingTimeText.enabled = true;
        remainingTimeText.color = Color.red;
        remainingTimeText.text = "Another in opening";
    }

    void ActivateInitialUnlock()
    { 

        initialFirstOpen.SetActive( true);
        initialFirstOpen.transform.GetChild(0).GetComponent<Text>().text = thisChest.openDuration.Hours+" hours";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisChest.chestState != ChestState.Ready)
        {
            Debug.Log("clicked chest");
            ChestClickedOn();
        }
    }


    public void OnExitClicked()
    {
        menuPopup.SetActive(false);
    }

    // Update is called once per frame
    void OnDestroy () {
        GetComponent<ChestMenuBehaviour>().OnChestAssigned -= AssignChest;
        GetComponent<ChestMenuBehaviour>().OnChestUnlocked -= UnlockAttempt;
        ChestMenuPopop.OnChestInOpening -= CheckChestStatus;
        ChestMenuBehaviour.OnChestReady -= AChestReady;
    }

  
}
