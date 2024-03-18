using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    public Enemy enemy;
    public GameObject explosion;

    void Update()
    {
        if (enemy.health <= 0)
        {
            Instantiate(explosion, enemy.gameObject.transform.position, Quaternion.identity);
            Invoke("DestroySelf", 0.001f);
  
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
