using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AD2020 : MonoBehaviour
{
    public string restartLevel = "HH_HUB";
    public string restartScene = "HH_MENU";
    public string bossScene = "HH_BOSS3";
    public float timer;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(restartScene);
        }

        if (!Input.anyKey)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer >= 300)
        {
            timer = 0;
            SceneManager.LoadScene(restartScene);
        }
    }
}
