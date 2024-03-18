using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnable : MonoBehaviour
{
    bool playerEnabled;
    public int interactBehaviour;
    private Interactables interactable;

    void Update()
    {
        if (playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact(interactBehaviour);
            }
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
    void Interact(int i)
    {
        interactable = GetComponent<Interactables>();

        switch (i)
        {
            case (0): //health pack
            default:

                interactable.HealthPickup();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (1): //ammo pack

                interactable.AmmoPickup();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (2): //quest menu

                interactable.QuestMenu();

                break;

            //-----------------------------------------------------------------------------------------------------------

            case (3):
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (4):
                break;

        }
    }
}
