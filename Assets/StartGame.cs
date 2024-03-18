using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        Invoke("ChangeScene", 3f);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("HH_MENU");
    }
}
