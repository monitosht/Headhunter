using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public GameObject creditsMenu;

    private void Start()
    {
        creditsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCredits();
        }
    }
    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
    }
}
