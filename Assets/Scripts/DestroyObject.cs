﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("Game Manager"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
