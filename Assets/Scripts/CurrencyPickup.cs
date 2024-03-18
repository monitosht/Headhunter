using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public enum PickupObject {credits};
    public PickupObject currentObject;
    public int value;

    public GameObject collisionPS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(currentObject == PickupObject.credits)
            {
                PlayerStats.credits += value;
                GameObject ps = Instantiate(collisionPS, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().PlayOneShot("CoinCollect");
                ps.transform.parent = other.gameObject.transform;
            }

            Destroy(gameObject);
        }
    }
}
