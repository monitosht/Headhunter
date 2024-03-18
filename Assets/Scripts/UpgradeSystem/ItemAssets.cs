using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform itemPrefab;

    public Sprite healthUp;
    public Sprite capacityUp;

    public GameObject healthUpScript;
    public GameObject capacityUpScript;
}
