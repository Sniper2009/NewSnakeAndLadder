using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabCoinHolder : MonoBehaviour {

    public CoinData CoinData;


    private void Start()
    {
       transform.GetChild(1).GetComponent<Text>().text = CoinData.coinAmount.ToString();
    }
}
