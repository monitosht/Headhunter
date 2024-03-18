using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovespeedUp : MonoBehaviour
{
    public float amount;

    private void Start()
    {
        FindObjectOfType<Upgrades>().MovespeedUp(amount);
    }
}
