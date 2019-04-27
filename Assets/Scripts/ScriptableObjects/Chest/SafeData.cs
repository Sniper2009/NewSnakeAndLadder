using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Safe/SafeData")]
public class SafeData : ScriptableObject {

	public Sprite safeImage;
    public float timeToOpenInSeconds;
    public int diceInside;

    public int safeType;//determines the shape
    public int safeID;//ID inside the type

}
