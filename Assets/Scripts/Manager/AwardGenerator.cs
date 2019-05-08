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

    [SerializeField] AwardGenerateData awardGenerate;

   

    private void Start()
    {
        ChestMenuBehaviour.OnPrizeOpen += GenerateChestAward;
    }

    void GenerateChestAward(SaveableChest chest)
    {
        ChestType chestType = chest.chestType;
        Prize prize = new Prize();
        prize.awardCards = new List<AwardCard>();
        int normalDiceNum = Random.Range(awardGenerate.lowerNormalDiceNum[(int)chestType], awardGenerate.higherNormalDiceNum[(int)chestType]+1);
        int rareDiceNum = Random.Range(awardGenerate.lowerRareDiceNum[(int)chestType], awardGenerate.higherRareDiceNum[(int)chestType]+1);
        int talismDiceNum = Random.Range(awardGenerate.lowerTalismDiceNum[(int)chestType], awardGenerate.higherTalismDiceNum[(int)chestType]+1);
        int coinNum = Random.Range(awardGenerate.lowerCoinNum[(int)chestType], awardGenerate.higherCoinNum[(int)chestType]+1);
        int gemNum = Random.Range(awardGenerate.lowerGemNum[(int)chestType], awardGenerate.higherGemNum[(int)chestType]+1);

        int normalCardNum = Random.Range(awardGenerate.normalLowDiceCard[(int)chestType], awardGenerate.normalHighDiceCard[(int)chestType]+1);
        int rareCardNum = Random.Range(awardGenerate.rareLowDiceCard[(int)chestType], awardGenerate.rareHighDiceCard[(int)chestType]+1);
        int talismCardNum = Random.Range(awardGenerate.talismLowDiceCard[(int)chestType], awardGenerate.talismHighDiceCard[(int)chestType]+1);

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

        if (diceNum > 0 && cardNum>0)
        {
            for (int i = 0; i < cardNum; i++)
            {
                AwardCard temp = new AwardCard();
                temp.prizeID = 1;
                temp.diceRareness = rareness;
                int index = Random.Range(0, diceList.Count);
                temp.diceID = diceList[index].diceID;
                diceList.RemoveAt(index);
                temp.prizeAmount = 1;
                newCards.Add(temp);
            }
            diceNum -= cardNum;
            while (diceNum > 0)
            {
                foreach (var item in newCards)
                {
                    int rand = Random.Range(0, 2);
                    if (rand > 0)
                    {
                        item.prizeAmount++;
                        diceNum--;
                    }
                    if (diceNum <= 0)
                        break;
                }
            }
        }

        return newCards;

    }



    private void OnDestroy()
    {
        ChestMenuBehaviour.OnPrizeOpen -= GenerateChestAward;
    }
}
