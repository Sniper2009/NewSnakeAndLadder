using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardGenerator : MonoBehaviour {

    public delegate void RetVoidArgInt(int amount);
    public static event RetVoidArgInt OnCoinGotten;
    public static event RetVoidArgInt OnGemGotten;

    public delegate void RetVoidArgAwardcard(AwardCard card);
    public static event RetVoidArgAwardcard OnAwardReceived;

    [SerializeField] DiceDesignCollection normalDices;
    [SerializeField] DiceDesignCollection rareDices;
    [SerializeField] DiceDesignCollection talismanDices;

    [SerializeField] List<int> lowerNormalDiceNum;
    [SerializeField] List<int> higherNormalDiceNum;

    [SerializeField] List<int> lowerRareDiceNum;
    [SerializeField] List<int> higherRareDiceNum;

    [SerializeField] List<int> lowerTalismDiceNum;
    [SerializeField] List<int> higherTalismDiceNum;

    [SerializeField] List<int> lowerCoinNum;
    [SerializeField] List<int> higherCoinNum;

    [SerializeField] List<int> lowerGemNum;
    [SerializeField] List<int> higherGemNum;

    [SerializeField] List<int> normalLowDiceCard;
    [SerializeField] List<int> normalHighDiceCard;

    [SerializeField] List<int> rareLowDiceCard;
    [SerializeField] List<int> rareHighDiceCard;

    [SerializeField] List<int> talismLowDiceCard;
    [SerializeField] List<int> talismHighDiceCard;

    private void Start()
    {
        ChestMenuBehaviour.OnPrizeOpen += GenerateChestAward;
    }

    void GenerateChestAward(SaveableChest chest)
    {
        ChestType chestType = chest.chestType;
        Prize prize = new Prize();
        prize.awardCards = new List<AwardCard>();
        int normalDiceNum = Random.Range(lowerNormalDiceNum[(int)chestType], higherNormalDiceNum[(int)chestType]);
        int rareDiceNum = Random.Range(lowerRareDiceNum[(int)chestType], higherRareDiceNum[(int)chestType]);
        int talismDiceNum = Random.Range(lowerTalismDiceNum[(int)chestType], higherTalismDiceNum[(int)chestType]);
        int coinNum = Random.Range(lowerCoinNum[(int)chestType], higherCoinNum[(int)chestType]);
        int gemNum = Random.Range(lowerGemNum[(int)chestType], higherGemNum[(int)chestType]);

        int normalCardNum = Random.Range(normalLowDiceCard[(int)chestType], normalHighDiceCard[(int)chestType]);
        int rareCardNum = Random.Range(rareLowDiceCard[(int)chestType], rareHighDiceCard[(int)chestType]);
        int talismCardNum = Random.Range(talismLowDiceCard[(int)chestType], talismHighDiceCard[(int)chestType]);

        if (coinNum>0)
        {
            AwardCard temp = new AwardCard();
            temp.prizeID = 0;
            temp.prizeAmount = coinNum;
            prize.awardCards.Add(temp);
            OnAwardReceived(temp);
         
        }
        List<DiceFullDesign> dicesTemp = new List<DiceFullDesign>();
        foreach (var item in normalDices.diceFullDesigns)
        {
            dicesTemp.Add(item);
        }
        List<AwardCard> temp2=(GenerateDice(normalDiceNum, normalCardNum, dicesTemp, DiceRareness.Normal));
        foreach (var item in temp2)
        {
            prize.awardCards.Add(item);
            OnAwardReceived(item);
        }

         dicesTemp = new List<DiceFullDesign>();
        foreach (var item in rareDices.diceFullDesigns)
        {
            dicesTemp.Add(item);
        }

        temp2 = (GenerateDice(rareDiceNum, rareCardNum, dicesTemp, DiceRareness.Rare));
        foreach (var item in temp2)
        {
            OnAwardReceived(item);
            prize.awardCards.Add(item);
        }

        dicesTemp = new List<DiceFullDesign>();
        foreach (var item in talismanDices.diceFullDesigns)
        {
            dicesTemp.Add(item);
        }

        temp2 = (GenerateDice(talismDiceNum, talismCardNum, dicesTemp, DiceRareness.Talisman));
        foreach (var item in temp2)
        {
            OnAwardReceived(item);
            prize.awardCards.Add(item);
        }

        if (gemNum > 0)
        {
            AwardCard temp = new AwardCard();
            temp.prizeID =2;
            temp.prizeAmount = gemNum;
            prize.awardCards.Add(temp);
            OnAwardReceived(temp);
         
        }


    }

    List<AwardCard> GenerateDice(int diceNum, int cardNum, List<DiceFullDesign> diceList,DiceRareness rareness)
    
    {
        List<AwardCard> newCards = new List<AwardCard>();
        for (int i = 0; i < cardNum; i++)
        {
            AwardCard temp = new AwardCard();
            temp.prizeID = 1;
            temp.diceRareness = rareness;
            int index = Random.Range(0, diceList.Count);
            temp.diceID =diceList[ index].diceID;
            diceList.RemoveAt(index);
            temp.prizeAmount = 1;
            newCards.Add(temp);
        }
        diceNum -= cardNum;
        while (diceNum > 0)
        {
            foreach (var item in newCards)
            {
                item.prizeAmount++;
                diceNum--;
                if (diceNum <= 0)
                    break;
            }
        }

        return newCards;

    }



    private void OnDestroy()
    {
        ChestMenuBehaviour.OnPrizeOpen -= GenerateChestAward;
    }
}
