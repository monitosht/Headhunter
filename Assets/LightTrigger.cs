using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private GameObject roomLight;   
    // Start is called before the first frame update
    void Awake()
    {
        roomLight = transform.GetChild(0).gameObject;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomLight.SetActive(true);
        }
    }
}
