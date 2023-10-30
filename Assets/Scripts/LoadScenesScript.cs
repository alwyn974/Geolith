using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void CreditsGame()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
