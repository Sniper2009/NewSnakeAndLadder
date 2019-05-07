using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAwardReceive : MonoBehaviour {

    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiceAdd;
    public static event RetVoidArgSavedice OnDiceUpdate;

    [SerializeField] DiceSaver diceSaver;
    [SerializeField] DiceDefaultHolder defaultHolder;

    List<SaveableDice> dices;

	// Use this for initialization
	void Start () {
        dices = diceSaver.GetAllDices();
        AwardGenerator.OnAwardReceived += PrizeManage;
	}
	

    void PrizeManage(AwardCard card)
    {
        if(card.prizeID==1)//dice
        {


            AwardDice(card.diceID, card.prizeAmount);
        }
    }

    void AwardDice(int diceID, int amount)
    {
        Debug.Log("dice award");
        bool existingDice=false;
        foreach (var dice in dices)
        {
            if(dice.diceID==diceID)
            {
                existingDice = true;
                dice.amountAwarded += amount;
                OnDiceUpdate(dice);
                break;
            }
        }

        if(existingDice==false)
        {
            SaveableDice newDice = new SaveableDice();
            newDice.diceID = diceID;
            newDice.currentCharge = defaultHolder.maxChargePerLevel[0];
            newDice.amountAwarded = amount;
            OnDiceAdd(newDice);
        }

    }

    private void OnDestroy()
    {
        AwardGenerator.OnAwardReceived -= PrizeManage;
    }
}
