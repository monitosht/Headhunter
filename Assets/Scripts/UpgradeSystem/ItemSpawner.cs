using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemOld item;

    private void Start()
    {
        ItemWorld.SpawnItem(transform.position, item);
        Destroy(gameObject);
    }
}
