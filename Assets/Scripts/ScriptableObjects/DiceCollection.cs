using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dice/DiceCollection")]

public class DiceCollection : ScriptableObject {

    public List<DiceData> dices;
    public int currentDice;

    private void OnEnable()
    {

    }
}
