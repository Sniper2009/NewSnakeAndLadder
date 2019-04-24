using System.Collections;
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

    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public event RetVoidArgSavedice OnDiceAssigned;

    [SerializeField] Sprite emptySlot;
    [SerializeField] Sprite withDiceSlot;

    [SerializeField] int slotID;

    [SerializeField]Image borderImage;

    Image backgroundImage;
    DiceSlotState slotState;

    SaveableDice currentDice;
    private void Start()
    {
        DiceUIMenu.OnDiceSelected += OnDiceSelected;
        DiceSlotSelect.OnSlotSelected += OnSlotClicked;
        slotState = DiceSlotState.Empty;
        backgroundImage = GetComponent<Image>();
        borderImage.enabled = false;
        backgroundImage.sprite = emptySlot;
    }

    void OnSlotClicked(int ID)
    {
        if(slotID!=ID)
        {
            borderImage.enabled = false;
            backgroundImage.sprite = emptySlot;
            slotState = DiceSlotState.Empty;
            return;
        }
       
            borderImage.enabled = true;
            slotState = DiceSlotState.Selected;
       

    }


  



    void OnDiceSelected(SaveableDice dice)
    {
        if(slotState==DiceSlotState.Selected)
        {
            if (currentDice != null)
                OnDiceDeselect(currentDice.diceID);
            currentDice = dice;
            borderImage.enabled = false;
            backgroundImage.sprite = withDiceSlot;
            PlayerPrefs.SetInt("diceSelect_" + slotID,dice.diceID);
            OnDiceAssigned(dice);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        OnSlotSelected(slotID);
    }

    private void OnDestroy()
    {
        DiceUIMenu.OnDiceSelected -= OnDiceSelected;
        DiceSlotSelect.OnSlotSelected -= OnSlotClicked;
    }

 
}
