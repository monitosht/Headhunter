using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUp : MonoBehaviour
{
    public float amount;
    private void Start()
    {
        FindObjectOfType<Upgrades>().DashUp(amount);
    }
}
