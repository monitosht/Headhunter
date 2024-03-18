using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.Find("Game Manager"));
    }
}
