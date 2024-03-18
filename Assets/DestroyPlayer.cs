using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    GameObject gameManager;
    public InventoryObject inventory;
    public bool clearStats;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if(gameManager != null)
        {
            Destroy(gameManager);
            inventory.Container.Clear();
        }

        if(clearStats == true)
        {
            PlayerStats.damageDealt = 0;
            PlayerStats.enemiesKilled = 0;
            PlayerStats.bossesKilled = 0;
            PlayerStats.damageTaken = 0;
            PlayerStats.healingDone = 0;
            PlayerStats.bulletsFired = 0;
            PlayerStats.itemsLooted = 0;
            PlayerStats.totalTime = 0;
            PlayerStats.commonItems = 0;
            PlayerStats.rareItems = 0;
            PlayerStats.epicItems = 0;
        }
    }
}
