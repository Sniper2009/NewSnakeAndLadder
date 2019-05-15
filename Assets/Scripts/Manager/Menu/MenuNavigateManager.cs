using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigateManager : MonoBehaviour {
    private void Awake()
    {
        Screen.SetResolution(900, 1600,false);
  
    }
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void Multiplayer()
    {
        SceneManager.LoadScene(2);
    }


    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
