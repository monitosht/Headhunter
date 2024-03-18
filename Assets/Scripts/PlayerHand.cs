using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    void Update()
    {
        transform.rotation = GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>().aim.rotation;
    }
}
