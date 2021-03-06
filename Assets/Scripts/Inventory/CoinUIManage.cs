﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManage : MonoBehaviour {

    [SerializeField] Text coinText;

    int coinamount;

    private void Start()
    {
       // PlayerPrefs.SetInt("Coin", 20000);
        coinamount = PlayerPrefs.GetInt("Coin");
        DiceInfoSelect.OnCoinCharge += CoinAMountChange;
        DiceInfoUI.OnChargeCoin += CoinAMountChange;
        AwardGenerator.OnAwardReceived += CheckPrize;
        ShopManager.OnShopChangedCoin += CoinAMountChange;
        coinText.text = coinamount.ToString();
    }



    void CoinAMountChange(int amount)
    {
        coinamount += amount;
        PlayerPrefs.SetInt("Coin", coinamount);
        coinText.text = coinamount.ToString();
    }


    void CheckPrize(AwardCard card)
    {
    if(card.prizeID==0)
        {
            CoinAMountChange(card.prizeAmount);
        }
    }




    private void OnDestroy()
    {
        DiceInfoSelect.OnCoinCharge -= CoinAMountChange;
        AwardGenerator.OnAwardReceived -= CheckPrize;
        DiceInfoUI.OnChargeCoin -= CoinAMountChange;
        ShopManager.OnShopChangedCoin -= CoinAMountChange;
    }
}
