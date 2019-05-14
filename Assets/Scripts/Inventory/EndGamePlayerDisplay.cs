using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePlayerDisplay : MonoBehaviour {

    public delegate void RetVoidArgChestSave(SaveableChest chest);
    public static event RetVoidArgChestSave OnAddChest;

    [SerializeField] ChestSaver chestSaver;

    [SerializeField] int playerNum;
    [SerializeField] Text coinAmount;
    [SerializeField] Image safeImage;
    [SerializeField] ChestCollection chestCollection;
    [SerializeField] Text safeFullText;

    [SerializeField] Image coinImage;


    private void Awake()
    {
        EndGameAnnounce.OnEndPanelActive += DisplayUIData;
    }

    void DisplayUIData(int winnerNum)
    {
        Debug.Log("ui:  " + playerNum);
        if (CoinCollection.playersCoin[playerNum] > 0)
        {
            coinAmount.enabled = true;
            coinImage.enabled = true;
            coinAmount.text = CoinCollection.playersCoin[playerNum].ToString();
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + CoinCollection.playersCoin[0]);
        }
        if (SafePickup.playerSafeID[0] > 0)
        {
            safeImage.enabled = true;

            safeImage.sprite = chestCollection.chestCollection[SafePickup.playerSafeID[0]].chestImage;
            if (chestSaver.ReturnChestNum() >= 4)
            {
                safeImage.color = Color.gray;
                safeFullText.enabled = true;
                return;
            }
        }
      
        ChestData cData = chestCollection.chestCollection[SafePickup.playerSafeID[0]];
        SaveableChest newChest = new SaveableChest(cData.chestID, ChestState.Closed, System.DateTime.Now, cData.openDuration,cData.chestType);
        if(playerNum==winnerNum && playerNum>0 && cData.chestID>0)
        OnAddChest(newChest);
    }

    private void OnDestroy()
    {
        EndGameAnnounce.OnEndPanelActive -= DisplayUIData;
    }


}
