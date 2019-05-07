using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Shop/Coin")]
public class CoinObject : ScriptableObject {

    public Sprite coinImage;
    public int gemValue;
    public int coinValue;
    public int coinID;
    public string description;
}
