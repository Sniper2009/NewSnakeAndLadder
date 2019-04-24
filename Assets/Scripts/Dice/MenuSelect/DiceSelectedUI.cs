using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelectedUI : MonoBehaviour {

    [SerializeField] DiceSlotSelect mySlot;
    SaveableDice thisDice;
    int childnum = 3;
	// Use this for initialization
	void Start () {
        mySlot.OnDiceAssigned += AssignDice;
	}

    void AssignDice(SaveableDice newDice)
    {
        thisDice = newDice;
        for (int i = 0; i < childnum; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        GetComponent<Image>().enabled = true;
        DisplayDiceInfo();
    }

    void DisplayDiceInfo()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = DiceImageReader.diceImages[thisDice.diceID];

        transform.GetChild(0).GetComponent<Text>().text = "level " + (thisDice.level + 1);
        transform.GetChild(1).GetComponent<Text>().text = thisDice.currentCharge + "/"+ DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];

        transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale =
        new Vector2((float)thisDice.amountAwarded / DiceDefaultHolder.awardForNextLevel[thisDice.level], 1);
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = thisDice.amountAwarded + "/" + DiceDefaultHolder.awardForNextLevel[thisDice.level];
    }


    private void OnDestroy()
    {
        mySlot.OnDiceAssigned -= AssignDice;
    }
}
