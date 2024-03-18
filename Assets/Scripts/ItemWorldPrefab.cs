using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldPrefab : MonoBehaviour
{
    private Item item;

    private void Awake()
    {
        item = GetComponent<Item>();
        GetComponent<SpriteRenderer>().sprite = item.item.itemSprite;
        transform.localScale = item.item.spriteScale;
    }
}
