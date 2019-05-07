using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceLevelup : MonoBehaviour {
    public delegate void RetVoidArgSavedice(SaveableDice newDice);
    public event RetVoidArgSavedice OnDiceUpdate;
    public static event RetVoidArgSavedice OnADiceUpdate;
    [SerializeField] Image levelUpImage;
    [SerializeField] Image barImage;

    SaveableDice dice;
    [SerializeField]Color nonUpgradeColor;
    [SerializeField]Color UpgradeColor;

    private void Start()
    {
        GetComponent<DiceUIMenu>().OnThisDiceAssigned += SetDice;
       


    }


    void SetDice(SaveableDice newDice)
    {
        dice = newDice;
        if (dice.amountAwarded >= DiceDefaultHolder.awardForNextLevel[dice.level])
        {
        
            levelUpImage.color = UpgradeColor;
            barImage.color = UpgradeColor;
       
        }

        else
        {
      
            levelUpImage.color = nonUpgradeColor;
            barImage.color = nonUpgradeColor;
        }
    }

    public void OnLevelUp()
    {
        

        if (dice.level < DiceDefaultHolder.maxChargePErLevelStatic.Count-1)
        {
            if (dice.amountAwarded >= DiceDefaultHolder.awardForNextLevel[dice.level])
            {
                dice.amountAwarded -= DiceDefaultHolder.awardForNextLevel[dice.level];
                dice.level++;
                levelUpImage.color = UpgradeColor;
                barImage.color = UpgradeColor;
                OnDiceUpdate(dice);
                OnADiceUpdate(dice);
            }
            else
            {
                levelUpImage.color = nonUpgradeColor;
                barImage.color = nonUpgradeColor;
            }
        }
    }


    private void OnDestroy()
    {
        GetComponent<DiceUIMenu>().OnThisDiceAssigned -= SetDice;
    }
}
