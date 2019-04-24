using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceMechanism : MonoBehaviour {

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnDiceRolled;
    public static event RetVoidArgInt OnDiceNumberCharged;

    public delegate bool RetBoolArgInt(int num);
    public static event RetBoolArgInt OnCheckDiceCharge;

    [SerializeField] DiceCollection diceCollection;
    [SerializeField] Image resultOutput;
    int currentDiceID;
    [SerializeField]int[] diceNumbers= { 3};
	// Use this for initialization
	void Start () {
    
        DiceSelect.OnDiceUpdate += ChangeDice;
        ChangeDice(0);
	}
	


    void ChangeDice(int ID)
    {
        diceNumbers = diceCollection.dices[ID].diceNumbers;
        resultOutput.sprite = DiceImageReader.diceImages[ID];
        currentDiceID = ID;
    }

    public void OnRollDice()
    {
        bool hasCharge = OnCheckDiceCharge(currentDiceID);
        if(hasCharge==false)
        {
            ChangeDice(0);
            return;
        }
        int resultIndex = Random.Range(0, diceNumbers.Length);
        int result = diceNumbers[resultIndex];
       // resultOutput.sprite = diceCollection.dices[currentDiceID].diceImages[resultIndex];
        OnDiceRolled(result);
        if(OnDiceNumberCharged!=null)
        OnDiceNumberCharged(currentDiceID);

    }


    private void OnDestroy()
    {
       
        DiceSelect.OnDiceUpdate -= ChangeDice;
    }
}
