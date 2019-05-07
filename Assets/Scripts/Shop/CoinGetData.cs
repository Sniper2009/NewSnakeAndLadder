using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinGetData : MonoBehaviour {
    public delegate void RetVoidArgInt(int ID);
    public static event RetVoidArgInt OnCoinPurchase;
    [SerializeField] int coinID;
    [SerializeField] Image thisImage;
    [SerializeField] Text gemValueText;
    [SerializeField] Text coinText;
    [SerializeField] Text descriptionText;
    CoinObject thisCoin;

    // Use this for initialization
    void Start () {

        RetreiveData();

	}
	

    void RetreiveData()
    {
        thisCoin = CoinObjectList.GetCoinInfo(coinID);
        thisImage.sprite = thisCoin.coinImage;
        gemValueText.text = thisCoin.gemValue.ToString();
        coinText.text = thisCoin.coinValue.ToString();
        descriptionText.text = thisCoin.description;


    }

    public void OnThisClicked()
    {
        OnCoinPurchase(coinID);
    }

}
