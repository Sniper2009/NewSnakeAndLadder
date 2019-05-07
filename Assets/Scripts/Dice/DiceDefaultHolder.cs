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

    public List<int> moneyForUpgrade;
    public List<int> moneyForCharge;

    public static List<int> moneyForUpgradeStatic;
    public static List<int> moneyForChargeStatic;


    public static List<int> awardForNextLevel;
    public static List<int> maxChargePErLevelStatic;
    public static List<DiceClassCharge> diceChargeTimeStatic;


    public void GetValues()
    {
        awardForNextLevel = new List<int>();
        maxChargePErLevelStatic = new List<int>();
        diceChargeTimeStatic = new List<DiceClassCharge>();
        moneyForChargeStatic = new List<int>();
        moneyForUpgradeStatic = new List<int>();
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
        i = 0;
        foreach (var item in moneyForUpgrade)
        {
            moneyForChargeStatic.Add(moneyForCharge[i]);
            moneyForUpgradeStatic.Add(item);
            i++;
        }
    }
}
