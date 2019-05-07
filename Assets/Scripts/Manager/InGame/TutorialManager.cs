using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] GameObject tutorialPanel;


    public void OnGameStart()
    {
        tutorialPanel.SetActive(false);
    }
}
