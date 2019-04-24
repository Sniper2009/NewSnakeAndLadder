using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


[CreateAssetMenu(menuName ="Dice/DiceDefault")]
public class DiceDefaultHolder : ScriptableObject {

   public List<int> maxChargePerLevel;
    public List<int> awardAmountForNextLevel;
    public static List<int> awardForNextLevel;
    public static List<int> maxChargePErLevelStatic;


    public void GetValues()
    {
        awardForNextLevel = new List<int>();
        maxChargePErLevelStatic = new List<int>();
        int i = 0;
        foreach (var item in awardAmountForNextLevel)
        {
            awardForNextLevel.Add(item);
            maxChargePErLevelStatic.Add(maxChargePerLevel[i]);
            i++;
        
        }
    }
}
