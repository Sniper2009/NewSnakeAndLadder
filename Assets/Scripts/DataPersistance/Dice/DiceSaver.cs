using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSaver : SaveableItem {
    public delegate void RetVoidArgDicesave(SaveableDice dice);
    public static event RetVoidArgDicesave OnDiceBrowse;

    public delegate void RetVoidArgVoid();
    public static event RetVoidArgVoid OnDicesEmpty;

    public static DiceSaver instance;
    public List<SaveableDice> userDices;

    string saveFolder;
    string saveItemFolder;
    string filePath;
    // Use this for initialization
    void Start () {

        //if(instance!=null)
        //{
        //    Destroy(this.gameObject);
        //}
        MenuSwipeManager.OnGoToDice += BrowseDice;
        DiceInfoSelect.OnUpdateDice += UpdateDice;
        PlayerDiceHolding.OnDecreaseCharge += UpdateDice;
        DiceInfoUpdate.OnDiceAdded += AddDice;
        DiceAwardReceive.OnDiceAdd += AddDice;
        DiceAwardReceive.OnDiceUpdate += UpdateDice;
        DiceLevelup.OnADiceUpdate += UpdateDice;
        DiceUIMenu.OnUpdateDice += UpdateDice;
       // DiceInfoUpdate.OnStartBrowse += BrowseDice;
       // DontDestroyOnLoad(gameObject);
        instance = this;

        saveFolder = Application.persistentDataPath + "/game_save";
        saveItemFolder = Application.persistentDataPath + "/game_save" + "/dices";
        Debug.Log(saveFolder);
        filePath = Application.persistentDataPath + "/game_save" + "/dices" + "/Dices.txt";
        BrowseDice();
    }

    public override void ResetValues()
    {

        userDices = new List<SaveableDice>();
        SaveItems(this);

    }

    public override void SaveItems(SaveableItem itemsToSave)
    {
        userDices = ((DiceSaver)itemsToSave).userDices;
//        Debug.Log("iiii: " + userDices.Count);
        SaveLoadManager.instance.SaveItems(saveFolder, saveItemFolder, filePath, this);

    }

    public DiceSaver LoadItems()//return the whole thing for possible extensions=> not just the int[]
    {

        SaveLoadManager.instance.LoadItems(filePath, this);
        return this;
    }


    void UpdateDice(SaveableDice newDice)
    {
//        Debug.Log("updated:  " + newDice.diceID + "   " + newDice.currentCharge);
      //  userDices = LoadItems().userDices;

        foreach (var dice in userDices)
      
        {
         //     Debug.Log("in id:  " +dice.diceID+"   "+dice.currentCharge);
            if (dice.diceID==newDice.diceID)
            {
                dice.currentCharge = newDice.currentCharge;
                dice.amountAwarded = newDice.amountAwarded;
                dice.level = newDice.level;
                Debug.Log("dice ch: " + dice.diceID + "   " + newDice.isCharging);
                dice.isCharging = newDice.isCharging;
                dice.startToChargeTime = newDice.startToChargeTime;
               // Debug.Log("in change: " + dice.currentCharge);
                    break;

            }
        }
        SaveItems(this);

        //Debug.Log("after update");
        //userDices = LoadItems().userDices;
        //for (int i = 0; i < userDices.Count; i++)
        //{
        //    Debug.Log("in id:  " + userDices[i].diceID+"    "+userDices[i].currentCharge);
           
        //}
    }

    void AddDice(SaveableDice newDice)
    {
        if (userDices == null)
            userDices = new List<SaveableDice>();
        userDices.Add(newDice);
        Debug.Log("in save: " + userDices.Count);
        SaveItems(this);
    }


    public SaveableDice GetDices(int ID)
    {
        userDices = LoadItems().userDices;

        foreach (var item in userDices)
        {
            if (item.diceID == ID)
                return item;
        }
        return null;
       // return userDices;
    }


    public List<SaveableDice> GetAllDices()
    {
        userDices = LoadItems().userDices;
        return userDices;
    }


    void BrowseDice()
    {
      //  Debug.Log("browse dice");
        userDices = LoadItems().userDices;
        if (userDices == null || userDices.Count == 0)
        {
            if(OnDicesEmpty!=null)
            OnDicesEmpty();
        }
        else
        {
            foreach (var dice in userDices)
            {
                if(OnDiceBrowse!=null)
                OnDiceBrowse(dice);
            }
        }

    }

  


    private void OnDestroy()
    {
        PlayerDiceHolding.OnDecreaseCharge -= UpdateDice;
        DiceInfoUpdate.OnDiceAdded -= AddDice;
        DiceAwardReceive.OnDiceAdd -= AddDice;
        DiceAwardReceive.OnDiceUpdate -= UpdateDice;
        DiceLevelup.OnADiceUpdate -= UpdateDice;
        DiceInfoSelect.OnUpdateDice -= UpdateDice;
        DiceUIMenu.OnUpdateDice -= UpdateDice;
        MenuSwipeManager.OnGoToDice -= BrowseDice;
        //  DiceInfoUpdate.OnStartBrowse -= BrowseDice;
    }
}
