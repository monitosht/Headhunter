using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealt : MonoBehaviour
{
    PlayerManager playerManager;
    PotionSystem potionSystem;

    public bool Bloodthirsty;
    public bool Scavenger;

    public float damageDealt;
    public float damageThreshold;
    public int amount;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        potionSystem = FindObjectOfType<PotionSystem>();
    }

    void Update()
    {
        if(damageDealt >= damageThreshold)
        {
            if(Bloodthirsty == true)
            {
                playerManager.Heal(amount);
            }
            if(Scavenger == true)
            {
                potionSystem.currentPots += amount;
            }

            damageDealt = 0;
        }
    }
}
