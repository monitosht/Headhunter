using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackStrength;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 dir = collision.transform.position - transform.position;
            rb.AddForce(dir.normalized * knockbackStrength, ForceMode2D.Impulse);

            if (collision.gameObject.GetComponent<Enemy>().health <= 0)
            {
                collision.gameObject.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
    }
}
