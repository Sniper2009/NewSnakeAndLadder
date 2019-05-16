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
        Debug.Log("fdddddddiiisssppplllaaayyy:  " + name);
        EndGameAnnounce.OnEndPanelActive += DisplayUIData;
    }

    void DisplayUIData(int winnerNum)
    {
        Debug.Log("ui:  " + playerNum);
        playerNum = PlayerMoveSync.localPlayerID;
        if (CoinCollection.playersCoin[playerNum] > 0)
        {
            coinAmount.enabled = true;
            coinImage.enabled = true;
            coinAmount.text = CoinCollection.playersCoin[playerNum].ToString();
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + CoinCollection.playersCoin[playerNum]);
        }
        if (playerNum != winnerNum)
            return;
        if (SafePickup.playerSafeID[playerNum] > 0)
        {
            safeImage.enabled = true;

            safeImage.sprite = chestCollection.chestCollection[SafePickup.playerSafeID[playerNum]].chestImage;
            if (chestSaver.ReturnChestNum() >= 4)
            {
                safeImage.color = Color.gray;
                safeFullText.enabled = true;
                return;
            }
        }
      
        ChestData cData = chestCollection.chestCollection[SafePickup.playerSafeID[playerNum]];
        SaveableChest newChest = new SaveableChest(cData.chestID, ChestState.Closed, System.DateTime.Now, cData.openDuration,cData.chestType);
        if(playerNum==winnerNum && playerNum>0 && cData.chestID>0)
        OnAddChest(newChest);
    }

    private void OnDestroy()
    {
        EndGameAnnounce.OnEndPanelActive -= DisplayUIData;
    }


}
