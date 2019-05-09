using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSaver : SaveableItem {


    public delegate void RetVoidArgSavechest(SaveableChest chest);
    public static event RetVoidArgSavechest OnNewChest;
    string saveFolder;
    string saveItemFolder;
    string filePath;
    //array representative of items that player hold
    public List<SaveableChest> userChests;

   
    public static ChestSaver instance;





    private void Start()
    {
       
        ChestMenuBehaviour.OnChestChanged += UpdateChestStatus;
        ChessMenuUI.OnChestChange += UpdateChestStatus;
        ChestMenuBehaviour.OnChestRemove += RemoveChest;
        DontDestroyOnLoad(this.gameObject);
        EndGamePlayerDisplay.OnAddChest += AddChest;
        instance = this;
        saveFolder = Application.persistentDataPath + "/game_save";
        saveItemFolder = Application.persistentDataPath + "/game_save" + "/chests";
        Debug.Log(saveFolder);
        filePath = Application.persistentDataPath + "/game_save" + "/chests" + "/Chests.txt";

        userChests = new List<SaveableChest>();

        DisplayMenu();
    }


    public  int ReturnChestNum()
    {
        return userChests.Count;
    }

    void DisplayMenu()
    {
        BrowseChests();
    }
    public override void ResetValues()
    {

        userChests = new List<SaveableChest>();
        SaveItems(this);

    }

    public override void SaveItems(SaveableItem itemsToSave)
    {
        userChests = ((ChestSaver)itemsToSave).userChests;

        SaveLoadManager.instance.SaveItems(saveFolder, saveItemFolder, filePath, this);

    }

    public ChestSaver LoadItems()//return the whole thing for possible extensions=> not just the int[]
    {

        SaveLoadManager.instance.LoadItems(filePath, this);
        return this;
    }

    public void AddChest(SaveableChest chest)
    {

        //  (int chestID, ChestState chestState, DateTime openOrderTimeInSystem, TimeSpan openDuration, Prize prize)
        SaveableChest newChest = new SaveableChest(chest.chestID, chest.chestState, chest.openOrderTimeInSystem, chest.openDurationSaveable,chest.chestType);
        userChests.Add(newChest);
        SaveItems(this);
    }


    void UpdateChestStatus(SaveableChest chest)
    {
        foreach (var userChest in userChests)
        {
            Debug.Log("update: " + userChest.chestID+"   "+chest.chestState+"   "+userChest.openDurationSaveable.seconds+"  "+chest.openDurationSaveable.seconds);
            if(userChest.chestID==chest.chestID&& userChest.openDurationSaveable==chest.openDurationSaveable)
            {
              
                userChest.chestState = chest.chestState;
                break;
            }
        }
        SaveItems(this);
    }



    void BrowseChests()
    {
        userChests = LoadItems().userChests;
        foreach (var chest in userChests)
        {
            SaveableChest nChest = new SaveableChest(chest.chestID, chest.chestState, chest.openOrderTimeSaveable, chest.openDurationSaveable,chest.chestType);
            OnNewChest(nChest);
        }
    }


    void RemoveChest(SaveableChest chest)
    {
        int index=0;
        foreach (var userChest in userChests)
        {
            if (userChest.chestID == chest.chestID &&  userChest.openDurationSaveable == chest.openDurationSaveable)
            {
                break;
            }
            index++;
        }

        userChests.RemoveAt(index);
        SaveItems(this);
      
    }
    private void OnDestroy()
    {
        EndGamePlayerDisplay.OnAddChest -= AddChest;
        ChestMenuBehaviour.OnChestChanged -= UpdateChestStatus;
        ChessMenuUI.OnChestChange -= UpdateChestStatus;
        ChestMenuBehaviour.OnChestRemove -= RemoveChest;
    }








}
