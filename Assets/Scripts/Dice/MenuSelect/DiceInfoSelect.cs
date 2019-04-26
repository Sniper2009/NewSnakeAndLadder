using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoSelect : MonoBehaviour {

    public delegate void RetVoidArgSaveDice(SaveableDice dice);
    public static event RetVoidArgSaveDice OnUpdateDice;

     GameObject userInfoPanel;
    [SerializeField] Text chargeAmount;
    [SerializeField] Image diceSprite;
    [SerializeField] DiceCollection diceCollection;
   // [SerializeField] Text 
    SaveableDice thisDice;

    private void Start()
    {
        userInfoPanel = transform.GetChild(0).gameObject;
        userInfoPanel.SetActive(false);
        DiceUIMenu.OnDiceClicked += GetDice;
        DiceSelectedUI.OnDiceInfoClicked += GetDice;
    }


    void GetDice(SaveableDice dice)
    {
        thisDice = dice;
    }
    public void OnActicateInfo()
    {
        userInfoPanel.SetActive(true);
        diceSprite.sprite = DiceImageReader.diceImages[thisDice.diceID];
    }

    public void OnExit()
    {
        userInfoPanel.SetActive(false);
    }


    public void ExitPanel()
    {
        userInfoPanel.SetActive(false);
    }

    public void DicePutForCharge()
    {
        //TODO Check coins
        thisDice.currentCharge = DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];
        OnUpdateDice(thisDice);

    }

    public void DiceUpdate()
    {
        if(thisDice.level<DiceDefaultHolder.maxChargePErLevelStatic.Count)
        thisDice.level++;
        Debug.Log("thisdice change: " + thisDice.diceID);
        OnUpdateDice(thisDice);
    }


    private void OnDestroy()
    {
        DiceUIMenu.OnDiceClicked -= GetDice;
        DiceSelectedUI.OnDiceInfoClicked -= GetDice;
    }
}
