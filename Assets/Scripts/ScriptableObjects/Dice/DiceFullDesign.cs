using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum DiceClass
{
    Level1,
    Level2,
    Level3
}

[System.Serializable]
public enum DiceRareness
{
    Normal,
    Rare,
    Talisman
}

[CreateAssetMenu(menuName ="Dice/DiceFullDesign")]
public class DiceFullDesign : ScriptableObject {

    public Sprite diceImage;
    public int diceID;
    public Color color;
    public List<int> nums;
    public string diceName;
    public string diceStory;
    public DiceClass diceClass;
    public DiceRareness diceRareness;


}
