using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public void buttonBehaviour(int i)
    {
        bool creditsOpen = false;

        switch (i)
        {
            case (0): //Play
            default:
                Time.timeScale = 1f;
                SceneManager.LoadScene("HH002");
                break;
            case (1): //Quit
                Time.timeScale = 1f;
                Application.Quit();
                break;
            case (2): //Menu
                Time.timeScale = 1f;
                SceneManager.LoadScene("MenuScene");
                break;
            case (3): //Level Select 1
                Time.timeScale = 1f;
                SceneManager.LoadScene("HH_LEVEL01");
                break;
            case (4): //
                break;
        }
    }
}
