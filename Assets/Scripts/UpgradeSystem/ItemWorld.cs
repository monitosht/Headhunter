using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItem(Vector3 position, ItemOld item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.itemPrefab, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private ItemOld item;
    private SpriteRenderer sr;
    public GameObject script;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetItem(ItemOld item)
    {
        this.item = item;
        sr.sprite = item.GetSprite();
        script = item.ItemScript();

    }

    public ItemOld GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
