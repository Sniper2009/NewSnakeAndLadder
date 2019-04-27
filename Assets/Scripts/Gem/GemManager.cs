using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour {

    int gemAmount;
    [SerializeField] Text gemText;


	// Use this for initialization
	void Start () {
        ChestMenuBehaviour.OnPrizeOpen += CheckPrize;
        ChestMenuBehaviour.OnChargeGem += UpdateGemAMount;
        gemAmount = PlayerPrefs.GetInt("Gem");
        gemText.text = gemAmount.ToString();
	}
	
	void UpdateGemAMount(int amount)
    {
        gemAmount += amount;
        gemText.text = gemAmount.ToString();
        PlayerPrefs.SetInt("Gem", gemAmount);
    }

    void CheckPrize(Prize prize)
    {
        if(prize.prizeType==2)
        {
            UpdateGemAMount(prize.gemAmount);
        }
    }

    private void OnDestroy()
    {
        ChestMenuBehaviour.OnPrizeOpen -= CheckPrize;
        ChestMenuBehaviour.OnChargeGem -= UpdateGemAMount;
    }
}
