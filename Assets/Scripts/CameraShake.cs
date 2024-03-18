using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{
    [Header("Constant Shake")]
    public bool constantShake;
    public float magnitude, length, multiplier, roughness, fadeintime, fadeouttime;

    private void Update()
    {
        if (constantShake == true)
        {
            SingleShake(magnitude, length, multiplier);
        }
    }
    public void SingleShake(float mag, float leng, float multi)
    {
        CameraController cam = FindObjectOfType<CameraController>();

        float x = Random.Range(-1f, 1f) * multi;
        float y = Random.Range(-1f, 1f) * multi;

        transform.localPosition = new Vector2(x, y);

        cam.Shake(transform.localPosition, mag, leng);
    }
}
