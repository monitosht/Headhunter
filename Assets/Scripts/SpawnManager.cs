﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(gameManager, transform.position, Quaternion.identity);
    }
}