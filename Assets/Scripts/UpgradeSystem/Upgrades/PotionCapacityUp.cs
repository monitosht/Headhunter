using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCapacityUp : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Upgrades>().PotionCapacityUp(1);
    }
}
