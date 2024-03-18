using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    private bool playerEnabled;

    public float force;

    void Update()
    {
        if (playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Rigidbody2D rb = ItemWorld.SpawnItem(transform.position, new ItemOld { itemType = ItemOld.ItemType.CapacityUp }).GetComponent<Rigidbody2D>();
                rb.AddForce(transform.position * force);

                ItemWorld.SpawnItem(transform.position, new ItemOld { itemType = ItemOld.ItemType.HealthUp });

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
