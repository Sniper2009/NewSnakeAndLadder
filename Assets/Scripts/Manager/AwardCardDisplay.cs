using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AwardCardDisplay : MonoBehaviour,IPointerDownHandler {

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
    bool startedDisplay = false;

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
        if(startedDisplay==false)
        {
            DisplayCard(cardsToDisplay[0]);
            cardPanel.SetActive(true);
            startedDisplay = true;
        }
    }

  


    public void GoToNext()
    {
        if(cardsToDisplay.Count>0)
        {
            DisplayCard(cardsToDisplay[0]);

        }
        else
        {
            cardPanel.SetActive(false);
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


    void DisplayCard(AwardCard card)
    {
        Debug.Log("in display  "+card.prizeID);
        canShow = false;

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
     
        cardsToDisplay.RemoveAt(0);
       // cardPanel.SetActive(false);
       

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GoToNext();
    }

    private void OnDestroy()
    {
        AwardGenerator.OnAwardReceived -= AddToCards;
    }

  
}
