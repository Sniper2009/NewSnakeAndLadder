using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceUIMenu : MonoBehaviour,IPointerDownHandler {

    //child0: level| chil1=> charge| child2=> awarded, child 0=> fill image , child1=> fill text
    public delegate void RetVoidArgSavedice(SaveableDice dice);
    public static event RetVoidArgSavedice OnDiceSelected;
    public static event RetVoidArgSavedice OnDiceClicked;
    public static event RetVoidArgSavedice OnUpdateDice;
    public static event RetVoidArgSavedice OnDiceChargeStateChanged;
    public event RetVoidArgSavedice OnThisDiceAssigned;
    [SerializeField] DiceDesignApply diceUIPrefab;
    [SerializeField] Text diceChargingText;
    [SerializeField] Image chargeCoverImage;
    Color chargingColor;
    Color chargeDoneColor;
    [SerializeField] DiceDesignCollection diceDesignCollection;
   // [SerializeField]
    int totalAwarded;
    SaveableDice thisDice;
    public int thisDiceID;
    public bool diceInUse = false;

    System.TimeSpan remainingTime;
    System.DateTime startChargeTime;
    System.TimeSpan chargeDuration;

    [SerializeField] GameObject diceInfo;
    [SerializeField] GameObject diceUse;
    [SerializeField] GameObject diceDisplayDesign;
    [SerializeField] GameObject isChargingObject;
    [SerializeField] GameObject upgradeButton;

    bool isDiceSelected;

    int childNum = 3;
    public SaveableDice GetDice()
    {
        return thisDice;
    }

    private void Awake()
    {

        chargingColor = Color.white;
        chargingColor.a = 0.7f;
        chargeDoneColor = Color.white;
        chargeDoneColor.a = 0;
        DiceSlotSelect.OnStaticDiceAssigned += DiceSelected;
        DiceSlotSelect.OnTurnBorderOff += DeselectDice;
        DiceSaver.OnDiceBrowse += AssignDice;
        DiceSlotSelect.OnDiscardDice += CheckDiceDiscard;
        DiceSelectedUI.OnSlotDiceClicked += SelectedDicePanelClicked;
        OnDiceClicked += CheckDiceDeselect;
        DiceInfoSelect.OnUpdateDice += DiceUpdate;
        isDiceSelected = false;
        GetComponent<Image>().enabled = false;
        DiceInfoUpdate.OnDiceAdded += AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate += AssignDice;
    }


    void SelectedDicePanelClicked(int dummy)
    {
        diceUse.SetActive(false);
        diceInfo.SetActive(false);
    }

    void DiceUpdate(SaveableDice newDice)
    {
    
        if (thisDiceID == newDice.diceID)
        {
            Debug.Log("in edit");
            thisDice = newDice;
            DisplayDiceInfo();
        }
    }

    void AssignDice(SaveableDice dice)
    {
//        Debug.Log("new dice:  " + dice.diceID + "    " + thisDiceID);
        if (dice.diceID == thisDiceID)
        {

            //  Debug.Log("assiii:  " + dice.diceID + "   " + thisDiceID);
            thisDice = dice;
            for (int i = 0; i < childNum; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            DisplayDiceInfo();
            if (thisDice.isCharging)
                OnDiceChargeStateChanged(thisDice);
        
        }
    }


    void DisplayDiceInfo()
    {
        if (thisDice.isCharging)
        {
            Debug.Log("start charge");
            isChargingObject.SetActive(true);
            startChargeTime = new System.DateTime(thisDice.startToChargeTime.year, thisDice.startToChargeTime.month, thisDice.startToChargeTime.day,
                thisDice.startToChargeTime.hour, thisDice.startToChargeTime.minute, thisDice.startToChargeTime.seconds);
            diceChargingText.enabled = true;
            upgradeButton.SetActive(false);
            chargeCoverImage.color = chargingColor;
            foreach (var item in DiceDefaultHolder.diceChargeTimeStatic)
            {
                if(item.diceRareness==diceDesignCollection.diceFullDesigns[thisDice.diceID].diceRareness && item.diceLevel==thisDice.level)
                {
                    chargeDuration = new System.TimeSpan(item.chargeTime.hour, item.chargeTime.minute, item.chargeTime.seconds);
                    Debug.Log("got time:  " + chargeDuration);
                    break;
                }
            }
        }
        else
        {
            diceChargingText.enabled = false;
            chargeCoverImage.color = chargeDoneColor;
        }
        diceDisplayDesign.SetActive(true);
        GetComponent<Image>().enabled = true;
        diceUIPrefab.ChangeID(thisDice.diceID);
      //  GetComponent<Image>().sprite = DiceImageReader.diceImages[thisDiceID];

        transform.GetChild(0).GetComponent<Text>().text = "level " + (thisDice.level+1);
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = thisDice.currentCharge + "/"+DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];

        transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().localScale = 
        new Vector2(Mathf.Min(1,(float)thisDice.amountAwarded / DiceDefaultHolder.awardForNextLevel[thisDice.level]), 1);
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = thisDice.amountAwarded + "/" + DiceDefaultHolder.awardForNextLevel[thisDice.level];
    }


    void Update()
    {
        if (thisDice == null)
            return;
        if(thisDice.isCharging)
        {

            remainingTime = (startChargeTime+chargeDuration ) - System.DateTime.Now;
//                        Debug.Log("time: " + startChargeTime+ "   " + chargeDuration + "   " + System.DateTime.Now);
            diceChargingText.text = remainingTime.Hours.ToString() + ":" + remainingTime.Minutes.ToString()
                 + ":" + remainingTime.Seconds.ToString();

            if (remainingTime.Hours <= 0 && remainingTime.Minutes <= 0 && remainingTime.Seconds <= 0)
            {
                Debug.Log("is ready");
                thisDice.isCharging = false;
                thisDice.currentCharge = DiceDefaultHolder.maxChargePErLevelStatic[thisDice.level];
                diceChargingText.enabled = false;
                chargeCoverImage.color = chargeDoneColor;
                DisplayDiceInfo();
                OnUpdateDice(thisDice);
                isChargingObject.SetActive(false);
                OnDiceChargeStateChanged(thisDice);
                upgradeButton.SetActive(true);


            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (diceInUse == false && thisDice.isCharging==false)
        {
            OnDiceClicked(thisDice);

            diceUse.SetActive(true);
            diceInfo.SetActive(true);

            isDiceSelected = true;
        }

    }

    void DiceSelected(SaveableDice dice)
    {
        if(dice.diceID==thisDiceID)
            diceInUse = true;
    }

    public void OnUseClicked()
    {

        OnDiceSelected(thisDice);
    }

    void CheckDiceDiscard(SaveableDice selDice)
    {
        if(thisDiceID==selDice.diceID)
        {
            diceInUse = false;
        }
    }

    void DeselectDice(SaveableDice dice)
    {
        diceUse.SetActive(false);
        diceInfo.SetActive(false);
    }


    void CheckDiceDeselect(SaveableDice newDice)
    {
        if (thisDice == null)
            return;
//        Debug.Log("new: " + newDice + "   " + thisDice);
        if(newDice.diceID!=thisDice.diceID)
        {
            diceUse.SetActive(false);
            diceInfo.SetActive(false);
            isDiceSelected = false;
        }
    }



    private void OnDestroy()
    {
      
        DiceSlotSelect.OnStaticDiceAssigned -= DiceSelected;
        DiceSlotSelect.OnTurnBorderOff -= DeselectDice;
        DiceInfoUpdate.OnDiceAdded -= AssignDice;
        GetComponent<DiceLevelup>().OnDiceUpdate -= AssignDice;
        DiceSlotSelect.OnDiscardDice -= CheckDiceDiscard;
        DiceSaver.OnDiceBrowse -= AssignDice;
        OnDiceClicked -= CheckDiceDeselect;
        DiceInfoSelect.OnUpdateDice -= DiceUpdate;
        DiceSelectedUI.OnSlotDiceClicked -= SelectedDicePanelClicked;
    }

   
}
