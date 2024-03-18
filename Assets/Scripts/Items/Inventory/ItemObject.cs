using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum ItemRarity
{
    common,
    rare,
    epic,
}*/

[CreateAssetMenu(fileName = "newItem", menuName = "Items/item")]
public class ItemObject : ScriptableObject
{
    [Header("Item Text")]
    [TextArea(1, 1)]
    public string itemName;
    [TextArea(5, 15)]
    public string itemDescription;

    [Header("Item Stats")]
    //public ItemRarity rarity;
    [SerializeField] private Rarity rarity;
    public int itemPrice;

    [Header("item Info")]
    public Sprite itemSprite;
    public GameObject itemPrefab;
    public GameObject itemScript;
    public Vector2 spriteScale;

    public Rarity Rarity { get { return rarity; } }

    /*public string Name { get { return itemName; } }
    public string Description { get { return itemDescription; } }
    public string ColouredName
    {
        get
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
            return $"<color=#{hexColour}>{Name}</color>";
        }
    }*/
}
