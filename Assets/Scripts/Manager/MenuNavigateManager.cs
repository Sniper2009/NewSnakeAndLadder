using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigateManager : MonoBehaviour {

public void OnStartGame()
    {
        SceneManager.LoadScene(2);
    }


    public void DiceMenu()
    {
        SceneManager.LoadScene(1);
    }


    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
