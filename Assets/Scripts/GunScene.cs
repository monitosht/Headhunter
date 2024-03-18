using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScene : MonoBehaviour
{
    public bool gunScene;
    public bool cameraShake;

    void Update()
    {
        if(gunScene == false)
        {
            FindObjectOfType<GunController>().gunScene = false;
        }
        else
        {
            FindObjectOfType<GunController>().gunScene = true;
        }

        if(cameraShake == true)
        {
            FindObjectOfType<CameraShake>().constantShake = true;
        }
        else
        {
            FindObjectOfType<CameraShake>().constantShake = false;
        }
    }
}
