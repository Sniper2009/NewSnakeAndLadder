using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceImageReader : MonoBehaviour {

    public static List<Sprite> diceImages;
    [SerializeField] int diceNum;

	// Use this for initialization
	void Awake () {
        ReadImages();
	}
	
    void ReadImages()
    {
        for (int i = 0; i < diceNum; i++)
        {
            if (diceImages == null)
                diceImages = new List<Sprite>();
            diceImages.Add(Resources.Load<Sprite>("Dice/dice"+i));
         
        }
    }
}
