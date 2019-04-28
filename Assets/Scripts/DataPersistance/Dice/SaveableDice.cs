using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveableDice {

   public int diceID;
   public int level;
   public int currentCharge;
   public int amountAwarded;
    public int location;
    public bool isCharging;

    public DateTimeSaveable startToChargeTime;
}
