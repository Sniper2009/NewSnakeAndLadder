using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelect : MonoBehaviour {

    public delegate void RetVoidArgIntArray(List<int> moves);
    public static event RetVoidArgIntArray OnDiceNumbers;

    public static int playerDice = 0;
    [SerializeField] Text diceChangeError;
    public static int currentDiceID;
    [SerializeField] GameObject diceSelectPanel;
    [SerializeField] Image diceSceneImage;
    [SerializeField] DiceDesignCollection diceCollection;
    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnDiceUpdate;
	// Use this for initialization
	void Start () {
        GameTurnManager.OnPlayerDiceChange += CheckPlayerDice;
        diceSelectPanel.SetActive(false);
        DiceSelector.OnDiceSelected += UpdateDice;
	}
	
	void UpdateDice(int ID)
    {
       // if(currentDiceID)
        currentDiceID = ID;
       // OnDiceNumbers(diceCollection.dices[ID].diceNumbers);
    }


    void CheckPlayerDice(int i)
    {
        if (DiceSaver.instance.GetDices(currentDiceID).currentCharge <= 0)
        {
            currentDiceID = 0;
            playerDice = currentDiceID;
            diceChangeError.text = "";
           // diceSceneImage.sprite = diceCollection.diceFullDesigns[currentDiceID].diceImages[0];
            OnDiceUpdate(currentDiceID);
            OnDiceNumbers(diceCollection.diceFullDesigns[currentDiceID].nums);
            diceSelectPanel.SetActive(false);
        }
    }





    public void OnReturnGame()
    {
        if (DiceSaver.instance.GetDices(currentDiceID).currentCharge <= 0)
        {
            diceChangeError.text = "Dice out of charge!";
        }
        else
        {
            playerDice = currentDiceID;
            diceChangeError.text = "";
          //  diceSceneImage.sprite = diceCollection.dices[currentDiceID].diceImages[0];
            OnDiceUpdate(currentDiceID);
            OnDiceNumbers(diceCollection.diceFullDesigns[currentDiceID].nums);
            diceSelectPanel.SetActive(false);
        }
    }


    //public void OnDiceCharge()
    //{
    //    diceChangeError.text = "";
    //    GameTurnManager.currentPlayer.GetComponent<PlayerDiceHolding>().diceCharge[currentDiceID]++;
    //}

    public void OnGoToDiceSelect()
    {
        diceChangeError.text = "";
        diceSelectPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        GameTurnManager.OnPlayerDiceChange -= CheckPlayerDice;
        DiceSelector.OnDiceSelected -= UpdateDice;
    }
}
