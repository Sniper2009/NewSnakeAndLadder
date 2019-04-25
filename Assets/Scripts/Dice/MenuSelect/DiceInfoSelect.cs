using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInfoSelect : MonoBehaviour {

    [SerializeField] GameObject userInfoPanel;

	public void OnActicateInfo()
    {
        userInfoPanel.SetActive(true);
    }


    public void ExitPanel()
    {
        userInfoPanel.SetActive(false);
    }
}
