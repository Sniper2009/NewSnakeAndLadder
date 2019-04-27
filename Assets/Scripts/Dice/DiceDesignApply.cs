using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceDesignApply : MonoBehaviour {
    [SerializeField] DiceDesignCollection diceDesignCollection;
    [HideInInspector] public DiceFullDesign diceFullDesign;

    [SerializeField] Text nameText;
    [SerializeField] Text storyText;

    [SerializeField] GameObject threeFaces;
    [SerializeField] GameObject twoFaces;

    [SerializeField] Image threeDice;
    [SerializeField] Image twoDice;

    [SerializeField] List<Sprite> threeTopSprite;
    [SerializeField] List<Sprite> threeLeftSprite;
    [SerializeField] List<Sprite> threeRightSprite;

    [SerializeField] Image threeTop;
    [SerializeField] Image threeLeft;
    [SerializeField] Image threeRight;


    [SerializeField] List<Sprite> twoTopSprite;
    [SerializeField] List<Sprite> twoFrontSprite;


    [SerializeField] Image twoTop;
    [SerializeField] Image twoFront;

    [SerializeField] List<Sprite> classSprite;
    [SerializeField] Image diceClassImage;



    //private void Awake()
    //{
    //  //  ApplyChange();
    //}


    public void ChangeID(int ID)
    {
        Debug.Log("iiii:  " + ID);
        diceFullDesign = diceDesignCollection.diceFullDesigns[ID];
        ApplyChange();
    }

    void ApplyChange()
    {
        nameText.text = diceFullDesign.diceName;
        storyText.text = diceFullDesign.diceStory;
        diceClassImage.sprite = classSprite[(int)diceFullDesign.diceClass];
        Debug.Log("num:  " + diceFullDesign.nums.Count);
        if(diceFullDesign.nums.Count==2)
        {
            DesignTwoFace();
        }


        else
        {
            DesignThreeFace();
        }
    }


    void DesignTwoFace()
    {
        twoDice.color = diceFullDesign.color;
        twoFaces.SetActive(true);
        threeFaces.SetActive(false);
        twoTop.sprite = twoTopSprite[diceFullDesign.nums[0]];
        twoFront.sprite = twoTopSprite[diceFullDesign.nums[1]];
    }


    void DesignThreeFace()
    {
        threeDice.color = diceFullDesign.color;
        threeFaces.SetActive(true);
        twoFaces.SetActive(false);
        threeTop.sprite = threeTopSprite[diceFullDesign.nums[0]];
        threeLeft.sprite = threeLeftSprite[diceFullDesign.nums[1]];
        threeRight.sprite = threeRightSprite[diceFullDesign.nums[2]];
    }
}
