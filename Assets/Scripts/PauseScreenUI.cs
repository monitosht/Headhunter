using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseScreenUI : MonoBehaviour
{
    private PlayerManager playerManager;
    private GunController gunController;
    private PotionSystem potionSystem;
    private GunSwitch gunSwitch;

    public Slider healthBar;
    public TMP_Text healthBarText;

    public Text currentAmmo, maxAmmo, totalAmmo;
    public Text creditsText;

    public Text currentPotsText;
    public Text totalPotsText;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gunController = FindObjectOfType<GunController>();
        potionSystem = FindObjectOfType<PotionSystem>();
        gunSwitch = FindObjectOfType<GunSwitch>();
    }
    
    void Update()
    {
        currentAmmo.text = gunController.currentAmmo.ToString();
        maxAmmo.text = gunController.maxAmmo.ToString();
        creditsText.text = ": " + PlayerStats.credits.ToString();

        healthBar.maxValue = playerManager.maxHealth;
        healthBar.value = playerManager.health;
        healthBarText.text = playerManager.health + "/" + playerManager.maxHealth;

        currentPotsText.text = potionSystem.currentPots.ToString();
        totalPotsText.text = potionSystem.totalPots.ToString();

        if (gunSwitch.selectedGun == 0)
        {
            totalAmmo.text = "XXX";
        }
        else
        {
            totalAmmo.text = gunController.totalAmmo.ToString();
        }
    }
}
