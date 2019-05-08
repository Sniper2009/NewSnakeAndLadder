using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Award/AwardGenerateData")]
public class AwardGenerateData : ScriptableObject{

    public List<int> lowerNormalDiceNum;
    public List<int> higherNormalDiceNum;

    public List<int> lowerRareDiceNum;
    public List<int> higherRareDiceNum;

    public List<int> lowerTalismDiceNum;
    public List<int> higherTalismDiceNum;

    public List<int> lowerCoinNum;
    public List<int> higherCoinNum;

    public List<int> lowerGemNum;
    public List<int> higherGemNum;

    public List<int> normalLowDiceCard;
    public List<int> normalHighDiceCard;

    public List<int> rareLowDiceCard;
    public List<int> rareHighDiceCard;

    public List<int> talismLowDiceCard;
    public List<int> talismHighDiceCard;

}
