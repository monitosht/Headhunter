using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    public List<ItemOld> itemList;

    public Inventory()
    {
        itemList = new List<ItemOld>();
    }

    [HideInInspector] public bool itemInInventory = false;

    public void AddItem(ItemOld item)
    {
        foreach(ItemOld inventoryItem in itemList)
        {
            if(inventoryItem.itemType == item.itemType)
            {
                itemInInventory = true;
            }
        }
        if(itemInInventory == false)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        Debug.Log(itemList.Count);
    }

    public List<ItemOld> GetItemList()
    {
        return itemList;
    }


}
