using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceMechanism : MonoBehaviour {

    public delegate void RetVoidArgInt(int num);
    public static event RetVoidArgInt OnDiceRolled;
    public static event RetVoidArgInt OnDiceNumberCharged;

    public delegate bool RetBoolArgInt(int num);
    public static event RetBoolArgInt OnCheckDiceCharge;

    [SerializeField] DiceCollection diceCollection;
    [SerializeField] DiceDesignCollection diceDesignCollection;
    [SerializeField] Image resultOutput;
    [SerializeField] Image diceBackGround;

    [SerializeField] GameObject diceObject;
    int currentDiceID;
    int prevResult = 0;
    List<int> diceNumbers;
	// Use this for initialization
	void Start () {
    
        DiceSelector.OnDiceUpdate += ChangeDice;
        ChangeDice(0);
	}
	


    void ChangeDice(int ID)
    {
        diceNumbers = diceDesignCollection.diceFullDesigns[ID].nums;//diceCollection.dices[ID].diceNumbers;
        diceBackGround.color = diceDesignCollection.diceFullDesigns[ID].color;
        resultOutput.sprite = diceDesignCollection.resultSprite[prevResult];
        currentDiceID = ID;
    }

    public void OnRollDice()
    {
        bool hasCharge = OnCheckDiceCharge(currentDiceID);
        if(hasCharge==false)
        {
            ChangeDice(0);
            return;
        }
        int resultIndex = Random.Range(0, diceNumbers.Count);
        int result = diceNumbers[resultIndex];
        diceBackGround.color= diceDesignCollection.diceFullDesigns[currentDiceID].color;
        if(result<=6)
        resultOutput.sprite = diceDesignCollection.resultSprite[result];
        StartCoroutine(ShowDiceResult());
        OnDiceRolled(result);
        if(OnDiceNumberCharged!=null)
        OnDiceNumberCharged(currentDiceID);

    }

    IEnumerator ShowDiceResult()
    {
        diceObject.SetActive(true);

        yield return new WaitForSeconds(2);
        diceObject.SetActive(false);
    }


    private void OnDestroy()
    {
       
        DiceSelector.OnDiceUpdate -= ChangeDice;
    }
}
