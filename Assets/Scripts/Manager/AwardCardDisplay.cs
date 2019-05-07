using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardCardDisplay : MonoBehaviour {

    [SerializeField] Image awardImage;
    [SerializeField] Text awardDescription;
    [SerializeField] Text awardAmount;

    [SerializeField] Sprite coinSprite;
    [SerializeField] Sprite gemSprite;
    [SerializeField] DiceDesignCollection diceCollection;
    [SerializeField] string coinDescription;
    [SerializeField] string gemDescription;

    List<AwardCard> cardsToDisplay;
    bool canShow;
    [SerializeField] GameObject cardPanel;

	// Use this for initialization
	void Start () {
        canShow = true;
        cardsToDisplay = new List<AwardCard>();
        AwardGenerator.OnAwardReceived += AddToCards;
	}
	
	void AddToCards(AwardCard card)
    {
        Debug.Log("card addd: "+card.prizeID);
        cardsToDisplay.Add(card);
    }

    private void Update()
    {
        if(canShow==true && cardsToDisplay.Count>0)
        {
            StartCoroutine(DisplayCard(cardsToDisplay[0]));
        }
    }



    void DisplayCoin()
    {
        awardImage.sprite = coinSprite;
        awardDescription.text = coinDescription;

    }

    void DisplayGem()
    {
        awardImage.sprite = gemSprite;
        awardDescription.text = gemDescription;
    }

    void DisplayDice(int ID)
    {
        awardImage.sprite = diceCollection.diceFullDesigns[ID].diceImage;
        awardDescription.text = diceCollection.diceFullDesigns[ID].diceStory;
    }


    IEnumerator DisplayCard(AwardCard card)
    {
        Debug.Log("in display  "+card.prizeID);
        canShow = false;
        cardPanel.SetActive(true);
        awardAmount.text = card.prizeAmount.ToString();

        if(card.prizeID==0)
        {
            DisplayCoin();
        }

        if(card.prizeID==1)
        {
            DisplayDice(card.diceID);
        }

        if(card.prizeID==2)
        {
            DisplayGem();
        }
        yield return new WaitForSeconds(3);
        cardsToDisplay.RemoveAt(0);
        cardPanel.SetActive(false);
        canShow = true;

    }


    private void OnDestroy()
    {
        AwardGenerator.OnAwardReceived -= AddToCards;
    }
}
