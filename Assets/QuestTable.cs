using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTable : MonoBehaviour
{
    private bool playerEnabled;
    public GameObject questMenu;

    void Start()
    {
        questMenu.SetActive(false);
    }

    void Update()
    {
        if (playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (questMenu.activeInHierarchy == false)
                {
                    questMenu.SetActive(true);
                }
                else
                {
                    questMenu.SetActive(false);
                }
            }
        }
        else
        {
            questMenu.SetActive(false);
        }

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            questMenu.SetActive(false);
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = false;
        }
    }
}
