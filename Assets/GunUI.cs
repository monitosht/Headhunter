using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(FindObjectOfType<GunManager>() != null)
        {
            image.sprite = FindObjectOfType<GunManager>().gun.sprite;
        }
    }
}
