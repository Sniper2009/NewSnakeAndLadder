using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelectedUI : MonoBehaviour {
    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiceInfoClicked;

    public delegate void RetVoidArgInt(int ID);
    public static event RetVoidArgInt OnSlotDiceClicked;

    bool enableInfo = false;
    [SerializeField] DiceDesignApply diceDesignApply;
    [SerializeField] DiceSlotSelect mySlot;
    [SerializeField] GameObject infoButton;
    [SerializeField] GameObject designApplyObject;
    SaveableDice thisDice;
    int childnum = 3;
    int slotID;
	// Use this for initialization
	void Start () {
        transform.parent.GetComponent<DiceSlotSelect>().OnDisplayInfoButton += ClickedOn;
        slotID = transform.parent.GetComponent<DiceSlotSelect>().slotID;
        OnSlotDiceClicked += CheckSlotDice;
        mySlot.OnDiceAssigned += AssignDice;
        DiceUIMenu.OnDiceClicked += DiceInMenuclicked;
       
	}

   void DiceInMenuclicked(SaveableDice dummy)
    {
        infoButton.SetActive(false);
    }

    void ClickedOn(int i)
    {
        if (enableInfo == true)
        {
            infoButton.SetActive(true);
            OnSlotDiceClicked(slotID);
            OnDiceInfoClicked(thisDice);
        }
    }

    void CheckSlotDice(int clickedslot)
    {
        if(clickedslot!=slotID)
        {
            infoButton.SetActive(false);
        }
    }

    public void InfoClicked()
    {
        infoButton.SetActive(false);
    }

    void AssignDice(SaveableDice newDice)
    {
        enableInfo = true;
        thisDice = newDice;
        for (int i = 0; i < childnum; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
      //  GetComponent<Image>().enabled = true;
        DisplayDiceInfo();
    }

    void DisplayDiceInfo()
    {
       // GetComponent<Image>().enabled = true;
        designApplyObject.SetActive(true);
        diceDesignApply.ChangeID(thisDice.diceID);
        //GetComponent<Image>().sprite = DiceImageReader.diceImages[thisDice.diceID];

        transform.GetChild(0).GetComponent<Text>().text = "level " + (thisDice.level + 1);
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = thisDice.currentCharge + "/"+ DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];

        transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale =
         new Vector2(Mathf.Min(1.0f, (float)thisDice.amountAwarded / DiceDefaultHolder.awardForNextLevel[thisDice.level]), 1);
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = thisDice.amountAwarded + "/" + DiceDefaultHolder.awardForNextLevel[thisDice.level];
    }


    private void OnDestroy()
    {
        OnSlotDiceClicked -= CheckSlotDice;
        mySlot.OnDiceAssigned -= AssignDice;
        transform.parent.GetComponent<DiceSlotSelect>().OnDisplayInfoButton -= ClickedOn;
        DiceUIMenu.OnDiceClicked -= DiceInMenuclicked;

    }
}
