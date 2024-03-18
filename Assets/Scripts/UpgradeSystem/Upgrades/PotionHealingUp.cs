using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealingUp : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Upgrades>().PotionHealingUp(1);
    }
}
