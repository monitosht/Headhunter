using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoEmpty : MonoBehaviour
{
    GunController gun;
    PlayerManager playerManager;
    public GameObject text;
    public Text slashText;

    void Start()
    {
        gun = FindObjectOfType<GunController>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    void Update()
    {
        if(gun.totalAmmo == 0 && gun.currentAmmo == 0)
        {
            text.SetActive(true);
            playerManager.currentAmmo.color = Color.red;
            slashText.color = Color.red;
            playerManager.maxAmmo.color = Color.red;
            playerManager.totalAmmo.color = Color.red;
        }
        else
        {
            text.SetActive(false);
            playerManager.currentAmmo.color = Color.white;
            slashText.color = Color.white;
            playerManager.maxAmmo.color = Color.white;
            playerManager.totalAmmo.color = Color.white;
        }
    }
}
