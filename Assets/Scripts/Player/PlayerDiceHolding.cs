using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiceHolding : MonoBehaviour {

    public delegate void RetVoidArgSaveableDice(SaveableDice dice);
    public static event RetVoidArgSaveableDice OnDecreaseCharge;

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnDiceChargeUpdate;

    
  
    public int diceCharge;
    public int currentDiceID;
    SaveableDice currentDice;

    private void Start()
    {
       // Debug.Log("curr: " + DiceSaver.instance.GetDices().Count + "   ");
        
    }

    private void Update()
    {
        if (currentDice == null)
            currentDice = DiceSaver.instance.GetDices(0);
    }

    private void OnEnable()
    {
        DiceMechanism.OnDiceNumberCharged += decreaseDiceCharge;
        DiceMechanism.OnCheckDiceCharge += checkDiceCharge;
        DiceSelect.OnDiceUpdate += UpdateDice;

    }

    void decreaseDiceCharge(int diceNum)
    {
        if (diceNum == 0)
            return;
        currentDice.currentCharge--;
        Debug.Log("current charge: " + currentDice.currentCharge);
        OnDecreaseCharge(currentDice);
        OnDiceChargeUpdate();
        if (currentDice.currentCharge == 0)
        {
            currentDiceID = 0;

        }
    }

    void UpdateDice(int ID)
    {

        currentDice = DiceSaver.instance.GetDices(ID);
        currentDiceID = ID;
    }


    bool checkDiceCharge(int diceID)
    {
        if (DiceSaver.instance.GetDices(diceID).currentCharge > 0)
            return true;
        return false;
    }
    //void addToDice(int addAmount,int diceNum)
    //{
    //    diceCharge[diceNum] += diceNum;
    //}

    private void OnDisable()
    {
        DiceMechanism.OnDiceNumberCharged -= decreaseDiceCharge; 
        DiceMechanism.OnCheckDiceCharge -= checkDiceCharge;
        DiceSelect.OnDiceUpdate -= UpdateDice;
    }

    private void OnDestroy()
    {
        DiceMechanism.OnDiceNumberCharged -= decreaseDiceCharge;
        DiceMechanism.OnCheckDiceCharge -= checkDiceCharge;
        DiceSelect.OnDiceUpdate -= UpdateDice;
    }


}
