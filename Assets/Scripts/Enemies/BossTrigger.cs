using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    private float timer = 1f;
    private bool timerOn;

    void Update()
    {
        if(timer <= 0)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(timerOn == true)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timerOn = true;
    }
}
