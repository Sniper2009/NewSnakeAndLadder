using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoUI : MonoBehaviour {
    public delegate void RetVoidArgDice(SaveableDice dice);
    public static event RetVoidArgDice OnDiceUpgrade;

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnButtonClicked;

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnChargeCoin;
SaveableDice thisDice;

    [SerializeField] Image upgradeAmountBar;
    [SerializeField] Text upgradeText;
    [SerializeField] Color nonupgradeColor;
    [SerializeField] Color upgradeColor;
    [SerializeField] Image iconImage;

    [SerializeField] Image chargeAmountBar;
    [SerializeField] Text chargeText;

    [SerializeField] Image upgradeImage;
    [SerializeField] Button upgradeButton;

    [SerializeField] Image chargeImage;
    [SerializeField] Button ChargeButton;

    [SerializeField] Text upgradeMoneyText;
    [SerializeField] Text chargeMoneyText;

    [SerializeField] Text chargeTimeText;
    [SerializeField] DiceDesignCollection diceDesignCollection;

	// Use this for initialization
	void Awake () {

        DiceInfoSelect.OnAssignDice += GetDice;
    }
	
void GetDice(SaveableDice dice)
    {
        thisDice = dice;
        CheckForUIElements();

    }

void CheckForUIElements()
    {
        UpgradeDisplayFix();
        ChargeDisplayFix();
       
    }

    void UpgradeDisplayFix()
    {
        upgradeMoneyText.text = DiceDefaultHolder.moneyForUpgradeStatic[thisDice.level].ToString();
        upgradeAmountBar.fillAmount = (float)thisDice.amountAwarded / (float)DiceDefaultHolder.awardForNextLevel[thisDice.level];
        upgradeText.text = (float)thisDice.amountAwarded + "/" + (float)DiceDefaultHolder.awardForNextLevel[thisDice.level];
        if (thisDice.amountAwarded >= DiceDefaultHolder.awardForNextLevel[thisDice.level] )
        {
            upgradeImage.color = Color.yellow;
            upgradeButton.enabled = true;
            upgradeAmountBar.color = upgradeColor;
            iconImage.color = upgradeColor;
        }

        else
        {
            upgradeButton.enabled = false;
            upgradeImage.color = Color.gray;
            upgradeAmountBar.color = nonupgradeColor;
            iconImage.color = nonupgradeColor;
        }

        if( PlayerPrefs.GetInt("Coin") >= DiceDefaultHolder.moneyForUpgradeStatic[thisDice.level])
        {
            upgradeImage.color = Color.yellow;
            upgradeButton.enabled = true;
        }
    }



    void ChargeDisplayFix()
    {
        chargeMoneyText.text = DiceDefaultHolder.moneyForChargeStatic[thisDice.level].ToString();
        chargeAmountBar.fillAmount = (float)thisDice.currentCharge / (float)DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];
        chargeText.text= (float)thisDice.currentCharge +"/"+ (float)DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];
        foreach (var item in DiceDefaultHolder.diceChargeTimeStatic)
        {
            if (item.diceRareness == diceDesignCollection.diceFullDesigns[thisDice.diceID].diceRareness && item.diceLevel == thisDice.level)
            {
                chargeTimeText.text = item.chargeTime.hour.ToString();

                break;
            }
        }




        if (thisDice.currentCharge <= DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level] && PlayerPrefs.GetInt("Coin") >= DiceDefaultHolder.moneyForChargeStatic[thisDice.level])
        {
            chargeImage.color = Color.yellow;
            ChargeButton.enabled = true;
        }

        else
        {
            chargeImage.color = Color.gray;
            ChargeButton.enabled = false;
        }
    }


    public void OnUpgradeClicked()
    {
        if (thisDice.level < DiceDefaultHolder.maxChargePErLevelStatic.Count - 1)
        {
            if (thisDice.amountAwarded >= DiceDefaultHolder.awardForNextLevel[thisDice.level])
            {
                thisDice.amountAwarded -= DiceDefaultHolder.awardForNextLevel[thisDice.level];
                thisDice.level++;
                OnDiceUpgrade(thisDice);
            }

            else//buy with coin
            {
                thisDice.level++;
                OnDiceUpgrade(thisDice);

                OnChargeCoin(-DiceDefaultHolder.moneyForUpgradeStatic[thisDice.level]);
            }
            Debug.Log("button clicked");
            OnButtonClicked();

        }
    }


    private void OnDestroy()
    {
        DiceInfoSelect.OnAssignDice -= GetDice;
    }

}
