using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public int interactBehaviour;
    bool playerEnabled;
    public GameObject questMenu;

    void Update()
    {
        if (playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact(interactBehaviour);
            }
        }
        if (playerEnabled == false)
        {
            //questMenu.SetActive(false);
        }
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

    //-------------------------------------------------------------------------------------------------------------------

    void Interact(int i)
    {
        switch (i)
        {
            case (0): //health pack
            default:

                HealthPickup();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (1): //ammo pack

                AmmoPickup();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (2): //quest menu

                if (questMenu == null)
                {
                    return;
                }

                QuestMenu();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (3):
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (4):
                break;

        }
    }

    //-------------------------------------------------------------------------------------------------------------------

    public void HealthPickup()
    {
        FindObjectOfType<PlayerManager>().health++;

        Destroy(gameObject);
    }

    //-------------------------------------------------------------------------------------------------------------------

    public void AmmoPickup()
    {
        GunSwitch gs = FindObjectOfType<GunSwitch>();
        GunController gc = FindObjectOfType<GunController>();

        if (gs.selectedGun == 0)
        {
            gc.totalAmmo += (gc.maxAmmo * 10);
        }
        gc.totalAmmo += (gc.maxAmmo * 10);

        Destroy(gameObject);
    }

    //-------------------------------------------------------------------------------------------------------------------

    public void QuestMenu()
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

    //-------------------------------------------------------------------------------------------------------------------

}
