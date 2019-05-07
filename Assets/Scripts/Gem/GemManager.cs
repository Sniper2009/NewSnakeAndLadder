using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour {

    int gemAmount;
    [SerializeField] Text gemText;


	// Use this for initialization
	void Start () {
        AwardGenerator.OnAwardReceived += CheckPrize;
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

    void CheckPrize(AwardCard card)
    {
        if(card.prizeID==2)
        {
            UpdateGemAMount(card.prizeAmount);
        }
    }

    private void OnDestroy()
    {
        AwardGenerator.OnAwardReceived -= CheckPrize;
        ChestMenuBehaviour.OnChargeGem -= UpdateGemAMount;
    }
}
