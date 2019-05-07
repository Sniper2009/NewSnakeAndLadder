using System.Collections;
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
        AwardGenerator.OnAwardReceived += CheckPrize;
        coinText.text = coinamount.ToString();
    }



    void CoinAMountChange(int amount)
    {
        coinamount += amount;
        PlayerPrefs.SetInt("Coin", coinamount);  
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
    }
}
