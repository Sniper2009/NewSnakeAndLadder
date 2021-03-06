﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum DiceSlotState
{
    Empty,
    Selected,
    WithDice
}

public class DiceSlotSelect : MonoBehaviour,IPointerDownHandler {

    public delegate void RetVoidArgInt(int slot);
    public static event RetVoidArgInt OnSlotSelected;
    public static event RetVoidArgInt OnDiceDeselect;
    public event RetVoidArgInt OnDisplayInfoButton;

    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiscardDice;
    public static event RetVoidArgSavedice OnStaticDiceAssigned;
    public event RetVoidArgSavedice OnDiceAssigned;
    public static event RetVoidArgSavedice OnTurnBorderOff;
    public event RetVoidArgSavedice OnDiceUseStart;

    [SerializeField] Sprite emptySlot;
    [SerializeField] Sprite withDiceSlot;

    public int slotID;

    [SerializeField]Image borderImage;

    bool playerIsChoosingDice=false;
    bool canTakeDice = true;

    Image backgroundImage;
    DiceSlotState slotState;

    SaveableDice currentDice;
    SaveableDice currentDiceInMenuSelected;
    private void Start()
    {
        DiceUIMenu.OnDiceSelected += OnDiceSelected;
        DiceUIMenu.OnDiceClicked += TurnSlotOff;
        DiceInfoSelect.OnUpdateDice += EmptySlot;

        PlayerPrefs.SetInt("diceSelect_" + slotID, 0);


        OnTurnBorderOff += TurnSlotOff;
   
        slotState = DiceSlotState.Empty;
        backgroundImage = GetComponent<Image>();
        borderImage.enabled = false;
        backgroundImage.sprite = emptySlot;

    }

 


    void TurnSlotOff(SaveableDice dummy)
    {
        Debug.Log("turn off");
        borderImage.enabled = false;
        canTakeDice = false;
    }






    void OnDiceSelected(SaveableDice dice)
    {
        Debug.Log("activate:  ");
        currentDiceInMenuSelected = dice;
        borderImage.enabled = true;
        canTakeDice = true;
    }

    void EmptySlot(SaveableDice dice)
    {
        if (currentDice!=null&& dice.diceID == currentDice.diceID)
        {
            currentDice = null;
            backgroundImage.sprite = emptySlot;
            OnDiscardDice(dice);
            OnDiceAssigned(currentDice);
        }

    }

    void AssignDice(SaveableDice dice)
    {
        Debug.Log("assiiigggnnn");
            currentDice = dice;
            borderImage.enabled = false;
            backgroundImage.sprite = withDiceSlot;
            PlayerPrefs.SetInt("diceSelect_" + slotID, dice.diceID);
            OnDiceAssigned(dice);
        OnStaticDiceAssigned(dice);
        OnTurnBorderOff(dice);

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down  "+canTakeDice);
        if (canTakeDice == false)
        {
            OnDisplayInfoButton(0);
            return;
        }
       
        //OnSlotSelected(slotID);
        if (currentDiceInMenuSelected != null)
        {
            if(currentDice!=null)
            OnDiscardDice(currentDice);
            AssignDice(currentDiceInMenuSelected);
            currentDiceInMenuSelected = null;
        }
    }

    private void OnDestroy()
    {
        OnTurnBorderOff -= TurnSlotOff;
        DiceUIMenu.OnDiceSelected -= OnDiceSelected;
        DiceUIMenu.OnDiceClicked -= TurnSlotOff;
        DiceInfoSelect.OnUpdateDice -= EmptySlot;

    }

 
}
