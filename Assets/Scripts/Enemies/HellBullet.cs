using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    public float speed;
    public float lifeTime;
    public int damage;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Invoke("Destroy", lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

            Destroy();
        }
    }
}
