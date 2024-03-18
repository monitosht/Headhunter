using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCamera : MonoBehaviour
{
    private Canvas canvas;
    private Camera layerCam;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        layerCam = GameObject.FindGameObjectWithTag("LayerCam").GetComponent<Camera>();
    }
    void Update()
    {
        layerCam = GameObject.FindGameObjectWithTag("LayerCam").GetComponent<Camera>();
        canvas.worldCamera = layerCam;
    }
}
