using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoSelect : MonoBehaviour {

    public delegate void RetVoidArgSaveDice(SaveableDice dice);
    public static event RetVoidArgSaveDice OnUpdateDice;

    public delegate void RetVoidArgInt(int i);
    public static event RetVoidArgInt OnCoinCharge;

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

    public void DicePutForCharge()
    {
        if (PlayerPrefs.GetInt("Coins") > 100)//TODO replace fix
        {
            OnCoinCharge(-100);
            thisDice.currentCharge = DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];
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
        DiceUIMenu.OnDiceClicked -= GetDice;
        DiceSelectedUI.OnDiceInfoClicked -= GetDice;
    }
}
