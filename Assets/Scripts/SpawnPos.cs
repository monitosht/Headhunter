using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    private GameObject player;

    void Awake()
    {
        player = FindObjectOfType<GunController>().gameObject;

        player.transform.position = transform.position;

        Destroy(gameObject);
    }
}
