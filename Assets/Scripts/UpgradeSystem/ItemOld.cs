using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemOld
{
    public enum ItemType
    {
        HealthUp,
        CapacityUp,
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthUp: return ItemAssets.Instance.healthUp;
            case ItemType.CapacityUp: return ItemAssets.Instance.capacityUp;
        }
    }

    public GameObject ItemScript()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthUp: return ItemAssets.Instance.healthUpScript;
            case ItemType.CapacityUp: return ItemAssets.Instance.capacityUpScript;
        }
    }

}
