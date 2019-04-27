using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class DiceSelector : MonoBehaviour,IPointerDownHandler {
     Image Border;

    [SerializeField] Text currentPlayerCharge;
    [SerializeField] Image diceImage;
    [SerializeField] DiceDesignApply diceDesignApply;
    [SerializeField] GameObject diceDesignObject;
    [SerializeField] int slotID;
    bool hasDice = false;
    
    string diceInSlot = "diceSelect_";
    int diceID;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnDiceSelected;


    void Start()
    {
        diceInSlot += slotID;
        DiceSelector.OnDiceSelected += DiceBoundaryCheck;
        PlayerDiceHolding.OnDiceChargeUpdate += UpdateChargeText;
        diceID = PlayerPrefs.GetInt(diceInSlot);
        if (diceID == 0)
        {
            diceImage.sprite = null;
            hasDice = false;
        }
        else
        {
            diceDesignObject.SetActive(true);
            diceDesignApply.ChangeID(diceID);
            hasDice = true;
            UpdateChargeText();
           // diceImage.sprite = DiceImageReader.diceImages[diceID];
        }
       
       
    }

    void OnEnable()
    {
       
       
        if(Border==null)
        Border = transform.GetChild(1).GetComponent<Image>();

    }


    public void UpdateChargeText()
    {

     //   diceID = DiceSelect.playerDice[GameTurnManager.playerTurn];
     
        if (DiceSaver.instance.GetDices(diceID).currentCharge <= 0 && DiceSelect.playerDice==diceID)
        {
            Border.color = Color.red;
        }
        else if(DiceSaver.instance.GetDices(diceID).currentCharge > 0 && DiceSelect.playerDice == diceID)
        {
            Border.color = Color.green;
        }
        else
        {
            Border.color = Color.black;
        }
        //if (diceID == 0)
        //{
        //    currentPlayerCharge.text = "Remaining: inf";
        //}
        //else
        //{
            currentPlayerCharge.text = "Remaining: " + DiceSaver.instance.GetDices(diceID).currentCharge;
       // }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasDice == false)
            return;
        if (DiceSaver.instance.GetDices(diceID).currentCharge > 0)
        {
            Border.color = Color.green;

        }
        else
        {
            Border.color = Color.red;
        }
      //  Debug.Log()
        OnDiceSelected(diceID);
    }


    void DiceBoundaryCheck(int ID)
    {
        if(ID!=diceID)
        {
            Border.color = Color.black;
        }
    }


    void OnDestroy()
    {
        DiceSelector.OnDiceSelected -= DiceBoundaryCheck;
        PlayerDiceHolding.OnDiceChargeUpdate -= UpdateChargeText;
    }
}
