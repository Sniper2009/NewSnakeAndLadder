using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName ="Dice/DiceRarityData")]
public class DiceRarityData : ScriptableObject {

    public List<string> rarityNames;
    public List<Sprite> rarityImages;
}
