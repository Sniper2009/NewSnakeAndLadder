using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoSelect : MonoBehaviour {

    public delegate void RetVoidArgSaveDice(SaveableDice dice);
    public static event RetVoidArgSaveDice OnUpdateDice;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnCoinCharge;

    bool anotherCharging;

     GameObject userInfoPanel;
    [SerializeField] DiceRarityData diceRarityData;
    [SerializeField] DiceClassNames diceClassNames;
    [SerializeField] Text chargeAmount;
    [SerializeField] Image diceSprite;
    [SerializeField] Text diceNameText;
    [SerializeField] Text diceStoryText;
    [SerializeField] DiceCollection diceCollection;

    [SerializeField] Image diceClassImage;
    [SerializeField] Text DiceClassText;

    [SerializeField] Image diceRarityImage;
    [SerializeField] Text diceRarityText;

    [SerializeField] DiceDesignApply designApply;

    [SerializeField] GameObject chargeButton;
   // [SerializeField] Text 
    SaveableDice thisDice;

    private void Awake()
    {
        userInfoPanel = transform.GetChild(0).gameObject;
        userInfoPanel.SetActive(false);
        DiceUIMenu.OnDiceChargeStateChanged += ChangeCharging;
        DiceUIMenu.OnDiceClicked += GetDice;
        DiceSelectedUI.OnDiceInfoClicked += GetDice;
    }


    void GetDice(SaveableDice dice)
    {
        thisDice = dice;
    }
    public void OnActicateInfo()
    {
        if (anotherCharging)
        {
            chargeButton.SetActive(false);
        }
        else
        {
            chargeButton.SetActive(true);
        }
        userInfoPanel.SetActive(true);
        designApply.ChangeID(thisDice.diceID);
        diceNameText.text = designApply.diceFullDesign.diceName;
        diceStoryText.text = designApply.diceFullDesign.diceStory;
        diceClassImage.sprite = designApply.classSprite[(int)designApply.diceFullDesign.diceClass];
        DiceClassText.text = diceClassNames.diceClassNames[(int)designApply.diceFullDesign.diceClass];

        diceRarityText.text = diceRarityData.rarityNames[(int)designApply.diceFullDesign.diceRareness];
        diceRarityImage.sprite= diceRarityData.rarityImages[(int)designApply.diceFullDesign.diceRareness];
        //diceSprite.sprite = DiceImageReader.diceImages[thisDice.diceID];
    }


    

    public void OnExit()
    {
        userInfoPanel.SetActive(false);
    }


    public void ExitPanel()
    {
        userInfoPanel.SetActive(false);
    }

    void ChangeCharging(SaveableDice dice)
    {
        if (dice.isCharging)
            anotherCharging = true;
        else
            anotherCharging = false;
    }

    public void DicePutForCharge()
    {
       
        Debug.Log("charge clicked: " + PlayerPrefs.GetInt("Coin"));
        if (PlayerPrefs.GetInt("Coin") > 100)//TODO replace fix
        {
            anotherCharging = true;
            Debug.Log("in charge");
            OnCoinCharge(-100);
         
            thisDice.isCharging = true;
            System.DateTime current = System.DateTime.Now;
            thisDice.startToChargeTime = new DateTimeSaveable(current.Year, current.Month, current.Day, current.Hour, current.Minute, current.Second);
            OnUpdateDice(thisDice);
        }

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
        DiceUIMenu.OnDiceChargeStateChanged -= ChangeCharging;
        DiceUIMenu.OnDiceClicked -= GetDice;
        DiceSelectedUI.OnDiceInfoClicked -= GetDice;
    }
}
