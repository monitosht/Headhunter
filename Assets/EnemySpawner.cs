using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer = 1.5f;
    private bool done = false;
    public GameObject enemy;
    public GameObject spawnAnim;

    void Update()
    {
        if(spawnTimer <= 0 && done == false)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            done = true;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        if(done == true)
        {
            Destroy(gameObject);
        }
    }
}
