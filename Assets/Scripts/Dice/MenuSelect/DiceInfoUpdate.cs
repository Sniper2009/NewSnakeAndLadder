using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceInfoUpdate : MonoBehaviour {
    public delegate void RetVoidArgDiceSave(SaveableDice dice);
    public static event RetVoidArgDiceSave OnDiceAdded;

    //public delegate void RetVoidArgVoid();
    //public static event RetVoidArgVoid OnStartBrowse;
    [SerializeField] int maxDiceIDDefault;
    [SerializeField] DiceDefaultHolder defaultHolder;
	// Use this for initialization
	void Awake () {
        DiceSaver.OnDicesEmpty += AddDefaultDices;
        defaultHolder.GetValues();
       // OnStartBrowse();
	}
	
	


    void AddDefaultDices()
    {
        for (int i = 0; i < maxDiceIDDefault; i++)
        {
            AddDefaultDice(i);
        }
    }

    void AddDefaultDice(int diceID)
    {
        SaveableDice dice = new SaveableDice();
        dice.diceID = diceID;
        dice.level = 0;
        dice.currentCharge = defaultHolder.maxChargePerLevel[0];
        dice.amountAwarded = 0;
        dice.isCharging = false;
        OnDiceAdded(dice);

    }


    private void OnDestroy()
    {
        DiceSaver.OnDicesEmpty -= AddDefaultDices;
    }
}
