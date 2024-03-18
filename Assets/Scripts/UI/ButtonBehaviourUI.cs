using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviourUI : MonoBehaviour
{
    public void ButtonSelect()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }

    public void ButtonBehaviour(int i)
    {
        switch (i)
        {
            case (0): //Play
            default:
                Time.timeScale = 1;
                SceneManager.LoadScene("HH_HUB");
                break;

            case (1): //Options
                

                break;

            case (2): //Credits
                
                
                break;

            case (3): //Exit               
                Application.Quit();
                break;

            case (4): //Resume
                break;

            case (5): //Main Menu
                Time.timeScale = 1;
                SceneManager.LoadScene("HH_MENU");
                break;

            case (6): //Pause Options
                break;

            case (7): //return from options

                break;
            case (8): //boss fight
                SceneManager.LoadScene("HH_BOSS3");
                break;
        }
    }
}
