using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class DiceSelector : MonoBehaviour,IPointerDownHandler {

    public delegate void RetVoidArgIntArray(List<int> moves);
    public static event RetVoidArgIntArray OnDiceNumbers;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnDiceUpdate;

    Image Border;

    [SerializeField] Text currentPlayerCharge;
    [SerializeField] Image diceImage;
    [SerializeField] DiceDesignApply diceDesignApply;
    [SerializeField] GameObject diceDesignObject;
    [SerializeField] int slotID;
    [SerializeField] DiceDesignCollection dices;
    bool hasDice = false;
    
    string diceInSlot = "diceSelect_";
    int diceID;

   
    public static event RetVoidArgInt OnDiceSelected;


    void Start()
    {
       
        diceInSlot += slotID;
        DiceSelector.OnDiceSelected += DiceBoundaryCheck;
        DiceSelect.OnDiceUpdate += TurnDiceOn;
        PlayerDiceHolding.OnDiceChargeUpdate += UpdateChargeText;
        diceID = PlayerPrefs.GetInt(diceInSlot);

        if (slotID == 0)
        {
            diceDesignObject.SetActive(true);
            diceDesignApply.ChangeID(0);
            hasDice = true;
            UpdateBorder();
        }

        else
        {
            if (diceID == 0)
            {
                diceImage.sprite = null;
                hasDice = false;
            }
            else
            {
                diceImage.enabled = true;
                diceDesignObject.SetActive(true);
                diceDesignApply.ChangeID(diceID);
                hasDice = true;
                UpdateBorder();
                UpdateChargeText();
                // diceImage.sprite = DiceImageReader.diceImages[diceID];
            }
        }
       
    }

    void OnEnable()
    {
       
       
        if(Border==null)
        Border = transform.GetChild(1).GetComponent<Image>();

    }


    void UpdateBorder()
    {
        if (DiceSaver.instance.GetDices(diceID).currentCharge <= 0 && DiceSelect.playerDice == diceID)
        {
            Border.color = Color.red;
        }
        else if (DiceSaver.instance.GetDices(diceID).currentCharge > 0 && DiceSelect.playerDice == diceID)
        {
            Border.color = Color.green;
        }
        else
        {
            Border.color = Color.black;
        }
    }


    public void UpdateChargeText()
    {

        Debug.Log("in update dice");
     //   diceID = DiceSelect.playerDice[GameTurnManager.playerTurn];
     
       
        //if (diceID == 0)
        //{
        //    currentPlayerCharge.text = "Remaining: inf";
        //}
        //else
        //{
            currentPlayerCharge.text =  DiceSaver.instance.GetDices(diceID).currentCharge+"/"+DiceDefaultHolder.
            maxChargePErLevelStatic[DiceSaver.instance.GetDices(diceID).level];
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
        OnDiceUpdate(diceID);
        OnDiceNumbers(dices.diceFullDesigns[diceID].nums);
    }

    void TurnDiceOn(int ID)
    {
        if(ID==diceID)
        {
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
            OnDiceUpdate(diceID);
            OnDiceNumbers(dices.diceFullDesigns[diceID].nums);
        }
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
        DiceSelect.OnDiceUpdate -= TurnDiceOn;
    }
}
