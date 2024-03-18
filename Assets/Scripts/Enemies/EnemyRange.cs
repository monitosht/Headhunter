using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private FireBullets fireBullets;
    public float playerRange;
    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fireBullets = gameObject.GetComponent<FireBullets>();
        fireBullets.enabled = false;
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= playerRange)
        {
            fireBullets.enabled = true;
        }
    }
}
