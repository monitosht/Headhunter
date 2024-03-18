using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDrug : MonoBehaviour
{
    PotionSystem potionSystem;
    PlayerController playerController;
    public float dashAmount;
    public float speedAmount;

    private bool active;

    private void Start()
    {
        potionSystem = FindObjectOfType<PotionSystem>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (potionSystem.cooldown >= 0 && active == false)
        {
            playerController.startSpeed += speedAmount;
            playerController.moveSpeed = playerController.startSpeed;
            playerController.startRollSpeed += dashAmount;

            active = true;
        }
        if (potionSystem.cooldown <= 0 && active == true)
        {
            playerController.startSpeed -= speedAmount;
            playerController.moveSpeed = playerController.startSpeed;
            playerController.startRollSpeed -= dashAmount;

            active = false;
        }
    }
}
