using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceLevelup : MonoBehaviour {
    public delegate void RetVoidArgSavedice(SaveableDice newDice);
    public event RetVoidArgSavedice OnDiceUpdate;
    public static event RetVoidArgSavedice OnADiceUpdate;

    SaveableDice dice;

    public void OnLevelUp()
    {
        
        dice = GetComponent<DiceUIMenu>().GetDice();
        if (dice.level < DiceDefaultHolder.maxChargePErLevelStatic.Count)
        {
            if (dice.amountAwarded >= DiceDefaultHolder.awardForNextLevel[dice.level])
            {
                dice.amountAwarded -= DiceDefaultHolder.awardForNextLevel[dice.level];
                dice.level++;

                OnDiceUpdate(dice);
                OnADiceUpdate(dice);
            }
        }
    }
}
