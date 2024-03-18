using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public bool itemNotification;
    public bool gunNotification;
    public ItemObject item;
    public Gun gun;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI itemRarity;
    public Image itemSprite;

    void Start()
    {
        Invoke("DestroySelf", 4f);

        if(itemNotification == true && item != null)
        {
            itemName.text = item.itemName;
            itemDesc.text = item.itemDescription;
            itemSprite.sprite = item.itemSprite;
            itemRarity.text = item.Rarity.name;
            itemRarity.color = item.Rarity.TextColour;
            itemSprite.SetNativeSize();
        }
        if (gunNotification == true && gun != null)
        {
            itemName.text = gun.gunName;
            itemDesc.text = gun.gunDescription;
            itemSprite.sprite = gun.sprite;
            itemSprite.SetNativeSize();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
