using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public delegate void RetVoidArgInt(int amount);
    public static event RetVoidArgInt OnShopChangedCoin;
    public static event RetVoidArgInt OnshopChangedGem;
        

	// Use this for initialization
	void Start () {
        CoinGetData.OnCoinPurchase += OnCoinPurchase;
	}
	
	public void OnCoinPurchase(int coinID)
    {
        CoinObject coin = CoinObjectList.GetCoinInfo(coinID);
        if (PlayerPrefs.GetInt("Gem")>=coin.gemValue)
        {
            OnShopChangedCoin(coin.coinValue);
            OnshopChangedGem(-coin.gemValue);
         
        }
    }


    private void OnDestroy()
    {
        CoinGetData.OnCoinPurchase -= OnCoinPurchase;
    }
}
