using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGFX : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipX = player.transform.position.x < transform.position.x;
    }
}
