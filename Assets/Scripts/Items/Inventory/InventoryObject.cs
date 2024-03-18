using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public bool canDestroy;

    public void AddItem(ItemObject _item)
    {
        bool hasItem = false;
        canDestroy = true;

        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                hasItem = true;
                canDestroy = false;
                break;
            }
        }

        if (!hasItem)
        {
            canDestroy = true;
            Container.Add(new InventorySlot(_item));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;

    public InventorySlot(ItemObject _item)
    {
        item = _item;
    }
}
