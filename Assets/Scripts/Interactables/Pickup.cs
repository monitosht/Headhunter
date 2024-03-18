using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    /*PlayerEnable playerEnable;

    private void Start()
    {
        playerEnable = GetComponent<PlayerEnable>();
    }
    private void Update()
    {
        if(playerEnable.interact == true)
        {
            Interact();
        }
    }
    void Interact()
    {

    }*/












































}











/*bool playerEnabled;
public int healAmount;
public bool ammoPickup;
public bool healthPickup;

void Update()
{
    if(playerEnabled == true)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(healthPickup == true)
            {
                FindObjectOfType<PlayerManager>().health += healAmount;
            }
            if(ammoPickup == true)
            {
                GunSwitch gs = FindObjectOfType<GunSwitch>();
                GunController gc = FindObjectOfType<GunController>();

                /*if(gs.selectedGun == 0)
                {
                    gc.totalAmmo += (gc.maxAmmo * 10);
                }
                gc.totalAmmo += (gc.maxAmmo * 10);
            }
            Destroy(gameObject);
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
}*/
