using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CoinGetData.OnCoinPurchase += OnCoinPurchase;
	}
	
	public void OnCoinPurchase(int coinID)
    {
        CoinObject coin = CoinObjectList.GetCoinInfo(coinID);
        if (PlayerPrefs.GetInt("Gem")>=coin.gemValue)
        {
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") - coin.gemValue);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin.coinValue);
        }
    }


    private void OnDestroy()
    {
        CoinGetData.OnCoinPurchase -= OnCoinPurchase;
    }
}
