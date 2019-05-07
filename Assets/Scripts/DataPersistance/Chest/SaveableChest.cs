using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ChestState
{
    Closed,
    InOpening,
    Ready
}

[System.Serializable]
public class SaveableChest  {

    public int chestID;
    public ChestState chestState;
    public System.DateTime openOrderTimeInSystem;
    public DateTimeSaveable openOrderTimeSaveable;
    public DateTimeSaveable openDurationSaveable;
    public System.TimeSpan openDuration;
    public ChestType chestType;

  //  public Prize prize;

    public SaveableChest(int chestID, ChestState chestState, DateTimeSaveable openOrderTimeArg, DateTimeSaveable openDurationArg,ChestType chestType)
    {
        this.chestType = chestType;
        this.chestID = chestID;
        this.chestState = chestState;
        openDurationSaveable = openDurationArg;
        this.openOrderTimeInSystem = new DateTime(openOrderTimeArg.year, openOrderTimeArg.month, openOrderTimeArg.day, openOrderTimeArg.hour, openOrderTimeArg.minute, openOrderTimeArg.seconds);
        this.openDurationSaveable = openDurationArg;
      //  this.prize = prize;

        openDuration = new TimeSpan(openDurationArg.hour,openDurationArg.minute,openDurationArg.seconds);
      
        Debug.Log( " sss  " + openDuration);
       // openOrderTimeSaveable = new DateTimeSaveable(0, 0, 0, openOrderTimeInSystem.Hour, openOrderTimeInSystem.Minute, openOrderTimeInSystem.Second);
      
    }

    public SaveableChest(int chestID, ChestState chestState, DateTime openOrderTime, DateTimeSaveable openDurationArg,ChestType chestType)
    {
        this.chestType = chestType;
        this.chestID = chestID;
        this.chestState = chestState;
     
        this.openOrderTimeInSystem = openOrderTime;
        this.openDurationSaveable = openDurationArg;
       // this.prize = prize;

        openDuration = new TimeSpan(openDurationArg.hour, openDurationArg.minute, openDurationArg.seconds);

        Debug.Log(" sss  " + openDuration);
        openOrderTimeSaveable = new DateTimeSaveable(openOrderTimeInSystem.Year, openOrderTimeInSystem.Month, openOrderTimeInSystem.Day, 
        openOrderTimeInSystem.Hour, openOrderTimeInSystem.Minute, openOrderTimeInSystem.Second);

    }
}
