using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityCheck : MonoBehaviour
{
    public ItemObject item;
    void Start()
    {
        if (item.Rarity.Name == "Common")
        {
            PlayerStats.commonItems++;
        }
        if (item.Rarity.Name == "Rare")
        {
            PlayerStats.rareItems++;
        }
        if (item.Rarity.Name == "Epic")
        {
            PlayerStats.epicItems++;
        }
    }
}
