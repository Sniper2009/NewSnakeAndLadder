using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerDisplay : MonoBehaviour {

    public delegate void RetVoidArgChestSave(SaveableChest chest);
    public static event RetVoidArgChestSave OnAddChest;

    [SerializeField] int playerNum;
    [SerializeField] Text coinAmount;
    [SerializeField] Image safeImage;
    [SerializeField] ChestCollection chestCollection;


    private void Awake()
    {
        EndGameAnnounce.OnEndPanelActive += DisplayUIData;
    }

    void DisplayUIData()
    {
        Debug.Log("ui:  " + playerNum);
        coinAmount.text = CoinCollection.playersCoin[playerNum].ToString();
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + CoinCollection.playersCoin[0]);
        safeImage.sprite = chestCollection.chestCollection[SafePickup.playerSafeID[0]].chestImage;
        ChestData cData = chestCollection.chestCollection[SafePickup.playerSafeID[0]];
        SaveableChest newChest = new SaveableChest(cData.chestID, ChestState.Closed, System.DateTime.Now, cData.openDuration, cData.prize);
        if(playerNum>0)
        OnAddChest(newChest);
    }

    private void OnDestroy()
    {
        EndGameAnnounce.OnEndPanelActive -= DisplayUIData;
    }


}
