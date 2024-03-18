using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    GameObject player;
    PlayerController playerController;
    PlayerManager playerManager;
    GunController gunController;
    PotionSystem potionSystem;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        player = playerController.gameObject;
        playerManager = FindObjectOfType<PlayerManager>();
        gunController = FindObjectOfType<GunController>();
        potionSystem = FindObjectOfType<PotionSystem>();
    }

    public void HealthUp(float amount)
    {
        playerManager.maxHealth += amount;
        playerManager.health += amount;
    }
    
    public void PotionCapacityUp(int amount)
    {
        potionSystem.totalPots += amount;
        potionSystem.currentPots += amount;
    }

    public void PotionHealingUp(float amount)
    {
        potionSystem.healAmount += amount;
    }

    public void MovespeedUp(float amount)
    {
        playerController.startSpeed += amount;
        playerController.moveSpeed = playerController.startSpeed;
    }

    public void DashUp(float amount)
    {
        playerController.startRollSpeed += amount;
    }

    public void ReloadDown(float amount)
    {
        gunController.startReloadTime -= amount;
    }

    public void DamageUp(float amount)
    {
        gunController.damage *= amount;
    }

    [Header("Bloodthirsty")]
    public float damageThreshold;
    public float returnHealAmount;
    [HideInInspector] public float damageDealt;

    public void DamageDealtHeal()
    {
        if(damageDealt >= damageThreshold)
        {
            playerManager.Heal(returnHealAmount);
            damageDealt = 0;
        }
    }

    [Header("Scavenger")]
    public float damageThresholdS;
    public int returnPotionAmount;
    [HideInInspector] public float damageDealtS;

    public void DamageDealtPotion()
    {
        if (damageDealt >= damageThreshold)
        {
            potionSystem.currentPots += returnPotionAmount;
            damageDealt = 0;
        }
    }

    [Header("Sharpshooter")]
    public float damageIncrease;
    public float deviationDecrease;

    public void SlowedBuff()
    {
        float startDamage = gunController.damage;
        float newDamage = gunController.damage += damageIncrease;

        float startDeviation = gunController.deviation;
        float newDeviation = gunController.deviation /= deviationDecrease;
        

        if (playerController.slowed)
        {
            gunController.damage = newDamage;
            gunController.deviation = newDeviation;
        }
        else
        {
            gunController.damage = startDamage;
            gunController.deviation = startDeviation;
        }
    }

    public void Tenacious(float amount)
    {
        playerController.invulTimer += amount;
    }




}
