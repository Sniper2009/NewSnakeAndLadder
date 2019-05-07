using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Shop/CoinList")]
public class CoinObjectList : ScriptableObject {

    public List<CoinObject> coinObjects;
    public static List<CoinObject> coinObjectsStatic;


    private void OnEnable()
    {
        coinObjectsStatic = new List<CoinObject>();
        foreach (var item in coinObjects)
        {
            coinObjectsStatic.Add(item);
        }
    }


    public static CoinObject GetCoinInfo(int ID)
    {
        return coinObjectsStatic[ID];
    }
}
