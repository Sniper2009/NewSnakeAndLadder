using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class DiceClassCharge
{
    public DiceRareness diceRareness;
    public int diceLevel;
    public DateTimeSaveable chargeTime;

}

[System.Serializable]
[CreateAssetMenu(menuName ="Dice/DiceDefault")]
public class DiceDefaultHolder : ScriptableObject {

   public List<int> maxChargePerLevel;
    public List<int> awardAmountForNextLevel;
    public List<DiceClassCharge> diceChargeTime;


    public static List<int> awardForNextLevel;
    public static List<int> maxChargePErLevelStatic;
    public static List<DiceClassCharge> diceChargeTimeStatic;


    public void GetValues()
    {
        awardForNextLevel = new List<int>();
        maxChargePErLevelStatic = new List<int>();
        diceChargeTimeStatic = new List<DiceClassCharge>();
        int i = 0;
        foreach (var item in awardAmountForNextLevel)
        {

            awardForNextLevel.Add(item);
            maxChargePErLevelStatic.Add(maxChargePerLevel[i]);
            i++;
        
        }
        foreach (var item in diceChargeTime)
        {
            diceChargeTimeStatic.Add(item);
        }
    }
}
