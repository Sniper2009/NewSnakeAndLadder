using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceDesignApply : MonoBehaviour {
    [SerializeField] DiceDesignCollection diceDesignCollection;
    [HideInInspector] public DiceFullDesign diceFullDesign;

    [SerializeField] Text nameText;
    [SerializeField] Text storyText;

    [SerializeField] Image diceSprite;
    public List<Sprite> classSprite;
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
        diceSprite.sprite = diceFullDesign.diceImage;
        Debug.Log("num:  " + diceFullDesign.nums.Count);

    }


   
}
