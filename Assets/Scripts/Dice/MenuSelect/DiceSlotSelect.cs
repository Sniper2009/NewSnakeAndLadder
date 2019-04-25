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
    public static event RetVoidArgSavedice OnDiscardDice;
    public static event RetVoidArgSavedice OnStaticDiceAssigned;
    public event RetVoidArgSavedice OnDiceAssigned;
    public static event RetVoidArgSavedice OnTurnBorderOff;
    public event RetVoidArgSavedice OnDiceUseStart;

    [SerializeField] Sprite emptySlot;
    [SerializeField] Sprite withDiceSlot;

    [SerializeField] int slotID;

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
            return;
       
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

    }

 
}
