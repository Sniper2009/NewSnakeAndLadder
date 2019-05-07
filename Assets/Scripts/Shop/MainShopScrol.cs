using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShopScrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void CheckPositionForScroll(SwipeData swipe)
    {
        if(swipe.Direction==SwipeDirection.Down)
        {
            if(transform.position.y>=-380)
            {
                transform.Translate(0, -(swipe.StartPosition-swipe.EndPosition).magnitude, 0);
            }
        }

        if(swipe.Direction==SwipeDirection.Up)
        {
            if(transform.position.y<=450)
            {
                
                transform.Translate(0, (swipe.StartPosition - swipe.EndPosition).magnitude, 0);
            }
        }
    }
}
