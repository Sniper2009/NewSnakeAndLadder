using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManage : MonoBehaviour {

    [SerializeField] Text coinText;

    int coinamount = PlayerPrefs.GetInt("Coin");

    private void Start()
    {
        DiceInfoSelect.OnCoinCharge += CoinAMountChange;
        ChestMenuBehaviour.OnPrizeOpen += CheckPrize;
        coinText.text = coinamount.ToString();
    }



    void CoinAMountChange(int amount)
    {
        coinamount += amount;
        PlayerPrefs.SetInt("Coin", coinamount);  
    }


    void CheckPrize(Prize prize)
    {
    if(prize.prizeType==0)
        {
            CoinAMountChange(prize.coinAmount);
        }
    }




    private void OnDestroy()
    {
        DiceInfoSelect.OnCoinCharge -= CoinAMountChange;
        ChestMenuBehaviour.OnPrizeOpen -= CheckPrize;
    }
}
