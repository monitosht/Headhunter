using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Upgrades>().HealthUp(1);
    }
}
