using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Dice/DiceDesignCollection")]
public class DiceDesignCollection : ScriptableObject {

    public List<DiceFullDesign> diceFullDesigns;
    public List<Sprite> resultSprite;
}
