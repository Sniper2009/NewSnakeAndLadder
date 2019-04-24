using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceUIMenu : MonoBehaviour,IPointerDownHandler {

    //child0: level| chil1=> charge| child2=> awarded, child 0=> fill image , child1=> fill text
    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiceSelected;
    int totalAwarded;
    SaveableDice thisDice;
    public int thisDiceID;

    bool isDiceSelected;

    int childNum = 3;
    public SaveableDice GetDice()
    {
        return thisDice;
    }

    private void Start()
    {
        DiceSaver.OnDiceBrowse += AssignDice;
        DiceSlotSelect.OnDiceDeselect += DiceDeselected;
        isDiceSelected = false;
        GetComponent<Image>().enabled = false;
        DiceInfoUpdate.OnDiceAdded += AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate += AssignDice;
    }

    void AssignDice(SaveableDice dice)
    {
        if (dice.diceID == thisDiceID)
        {
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
        new Vector2((float)thisDice.amountAwarded / DiceDefaultHolder.awardForNextLevel[thisDice.level], 1);
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = thisDice.amountAwarded + "/" + DiceDefaultHolder.awardForNextLevel[thisDice.level];
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDiceSelected == false)
        {
            OnDiceSelected(thisDice);
            isDiceSelected = true;
        }
    }

    void DiceDeselected(int id)
    {
        if (thisDiceID == id)
            isDiceSelected = false;
    }

    private void OnDestroy()
    {
        DiceInfoUpdate.OnDiceAdded -= AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate -= AssignDice;
        DiceSlotSelect.OnDiceDeselect -= DiceDeselected;
        DiceSaver.OnDiceBrowse -= AssignDice;
    }

   
}
