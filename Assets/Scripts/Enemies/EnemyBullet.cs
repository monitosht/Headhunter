using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 dir;
    private Transform playerPos;
    public float speed;
    public int damage;
    public float lifeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        dir = rb.velocity = (playerPos.position - rb.gameObject.transform.position).normalized * speed;
    
        Invoke("DestroyBullet", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            DestroyBullet();
        }
        if (other.gameObject.CompareTag("Player"))
        {              
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
