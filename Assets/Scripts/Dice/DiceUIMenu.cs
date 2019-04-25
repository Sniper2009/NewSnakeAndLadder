using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceUIMenu : MonoBehaviour,IPointerDownHandler {

    //child0: level| chil1=> charge| child2=> awarded, child 0=> fill image , child1=> fill text
    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiceSelected;
    public static event RetVoidArgSavedice OnDiceClicked;
    int totalAwarded;
    SaveableDice thisDice;
    public int thisDiceID;
    public bool diceInUse = false;

    

    [SerializeField] GameObject diceInfo;
    [SerializeField] GameObject diceUse;

    bool isDiceSelected;

    int childNum = 3;
    public SaveableDice GetDice()
    {
        return thisDice;
    }

    private void Start()
    {
        DiceSlotSelect.OnStaticDiceAssigned += DiceSelected;
        DiceSlotSelect.OnTurnBorderOff += DeselectDice;
        DiceSaver.OnDiceBrowse += AssignDice;
        DiceSlotSelect.OnDiscardDice += CheckDiceDiscard;
        OnDiceClicked += CheckDiceDeselect;
        isDiceSelected = false;
        GetComponent<Image>().enabled = false;
        DiceInfoUpdate.OnDiceAdded += AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate += AssignDice;
    }

    void AssignDice(SaveableDice dice)
    {
       
        if (dice.diceID == thisDiceID)
        {

            //  Debug.Log("assiii:  " + dice.diceID + "   " + thisDiceID);
            thisDice = dice;
            for (int i = 0; i < childNum; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            DisplayDiceInfo();
        }
    }


    void DisplayDiceInfo()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = DiceImageReader.diceImages[thisDiceID];

        transform.GetChild(0).GetComponent<Text>().text = "level " + (thisDice.level+1);
        transform.GetChild(1).GetComponent<Text>().text = thisDice.currentCharge + "/"+DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];

        transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale = 
        new Vector2(Mathf.Min(1,(float)thisDice.amountAwarded / DiceDefaultHolder.awardForNextLevel[thisDice.level]), 1);
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = thisDice.amountAwarded + "/" + DiceDefaultHolder.awardForNextLevel[thisDice.level];
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (diceInUse == false)
        {
            OnDiceClicked(thisDice);

            diceUse.SetActive(true);
            diceInfo.SetActive(true);

            isDiceSelected = true;
        }

    }

    void DiceSelected(SaveableDice dice)
    {
        if(dice.diceID==thisDiceID)
            diceInUse = true;
    }

    public void OnUseClicked()
    {

        OnDiceSelected(thisDice);
    }

    void CheckDiceDiscard(SaveableDice selDice)
    {
        if(thisDiceID==selDice.diceID)
        {
            diceInUse = false;
        }
    }

    void DeselectDice(SaveableDice dice)
    {
        diceUse.SetActive(false);
        diceInfo.SetActive(false);
    }


    void CheckDiceDeselect(SaveableDice newDice)
    {
        if (thisDice == null)
            return;
//        Debug.Log("new: " + newDice + "   " + thisDice);
        if(newDice.diceID!=thisDice.diceID)
        {
            diceUse.SetActive(false);
            diceInfo.SetActive(false);
            isDiceSelected = false;
        }
    }



    private void OnDestroy()
    {
        DiceSlotSelect.OnStaticDiceAssigned -= DiceSelected;
        DiceSlotSelect.OnTurnBorderOff -= DeselectDice;
        DiceInfoUpdate.OnDiceAdded -= AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate -= AssignDice;
        DiceSlotSelect.OnDiscardDice -= CheckDiceDiscard;
        DiceSaver.OnDiceBrowse -= AssignDice;
        OnDiceClicked -= CheckDiceDeselect;
    }

   
}
