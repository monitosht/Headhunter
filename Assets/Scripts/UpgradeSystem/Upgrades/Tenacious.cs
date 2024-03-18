using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenacious : MonoBehaviour
{
    public float invulAmount;
    public float speedAmount;

    PlayerManager playerManager;
    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerManager = FindObjectOfType<PlayerManager>();

        FindObjectOfType<Upgrades>().Tenacious(invulAmount);
    }

    private bool active;

    private void Update()
    {
        if (playerManager.invulTimer >= 0 && active == false)
        {
            playerController.startSpeed += speedAmount;
            playerController.moveSpeed = playerController.startSpeed;

            active = true;
        }

        if (playerManager.invulTimer <= 0 && active == true)
        {
            playerController.startSpeed -= speedAmount;
            playerController.moveSpeed = playerController.startSpeed;

            active = false;
        }
    }
}
