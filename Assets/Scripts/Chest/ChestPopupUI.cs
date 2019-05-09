using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PersianSupportForSilverlight;




public class ChestPopupUI : MonoBehaviour {

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnUnlockClicked;
   


    public delegate void RetVoidArgInt(int a);
    public static event RetVoidArgInt OnGemClicked;

    [SerializeField] Image chestImage;
    [SerializeField] Text chestName;
    [SerializeField] Text minRareDice;
    [SerializeField] Text coinText;
    [SerializeField] Text totalDice;
    [SerializeField] List<string> persianPhrases;
    [SerializeField] Text gemToPay;
    [SerializeField] int minutePerGem;
    [SerializeField] GameObject popupMenuPanel;

    private PersianMaker pm = new PersianMaker();
    [SerializeField] AwardGenerateData awardGenerate;
    [SerializeField] ChestCollection chestCollection;

    SaveableChest thisChest;
    int index;
    int calculatedGem;

    System.TimeSpan remainingTime;

    void Awake()
    {
        GetComponent<ChestMenuPopop>().OnChestAssigned += SetCommonUI;
    }

    void SetCommonUI(SaveableChest chest)
    {
        thisChest = chest;
        index= (int)thisChest.chestType;
        chestImage.sprite = chestCollection.chestCollection[thisChest.chestID].chestImage;
        chestName.text = chestCollection.chestCollection[thisChest.chestID].chestDescription;
        coinText.text = awardGenerate.lowerCoinNum[index] + persianPhrases[0] + awardGenerate.higherCoinNum[index]
        +persianPhrases[1];
        totalDice.text = (awardGenerate.lowerNormalDiceNum[index] + awardGenerate.rareLowDiceCard[index] + awardGenerate.talismLowDiceCard[index]) + persianPhrases[2];
        minRareDice.text = persianPhrases[3]+awardGenerate.rareLowDiceCard[index]+persianPhrases[4];

    }



    private void Update()
    {
        if(thisChest!=null && popupMenuPanel.activeSelf)
        gemToPay.text = CalculateGemRemaining().ToString();
    }

    int CalculateGemRemaining()
    {
        remainingTime = (thisChest.openOrderTimeInSystem + thisChest.openDuration) - System.DateTime.Now;
        calculatedGem = (remainingTime.Hours) * (60/minutePerGem) + remainingTime.Minutes / minutePerGem + 1;
        return calculatedGem;
    }
    public void UnlockClicked()
    {
        OnUnlockClicked();
    }

    public void GemClicked()
    {
        OnGemClicked(calculatedGem);
    }
    private void OnDestroy()
    {
        GetComponent<ChestMenuPopop>().OnChestAssigned += SetCommonUI;
    }
}
