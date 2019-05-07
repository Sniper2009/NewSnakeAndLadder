using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AwardCard
{

    public int prizeID;//coin, dice, gem
    public int diceID;
    public int prizeAmount;
    public DiceRareness diceRareness;
}

[System.Serializable]
public class Prize  {
    public List<AwardCard> awardCards;

}
