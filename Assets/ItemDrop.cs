using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private bool playerEnabled;

    public float force;
    public GameObject[] item;

    void Update()
    {
        if (playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Rigidbody2D rb = Instantiate(item[Random.Range(0, item.Length)], transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                rb.AddForce(transform.position * force);
                PlayerStats.itemsLooted++;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerEnabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerEnabled = false;
    }
}
