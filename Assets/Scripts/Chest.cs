using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerEnabled;
    public GameObject[] drops;
    public GameObject[] pickups;
    private int randomDrop;

    // Start is called before the first frame update
    void Start()
    {
        randomDrop = Random.Range(0, drops.Length);
        //Debug.Log(randomDrop);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Instantiate(drops[randomDrop], transform.position, Quaternion.identity);
                //Instantiate(pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
                ItemWorld.SpawnItem(transform.position, new ItemOld { itemType = ItemOld.ItemType.CapacityUp });
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
