using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    public string textString;
    public Text text;
    public float fadeTime;
    private bool display;
    
    void Start()
    {
        text.color = Color.clear;
    }
    void Update()
    {
        FadeText();
    }
    private void OnMouseOver()
    {
        display = true;
    }
    private void OnMouseExit()
    {
        display = false;
    }
    void FadeText()
    {
        if (display)
        {
            text.text = textString;
            text.color = Color.Lerp(text.color, Color.white, fadeTime * Time.deltaTime);
        }
        else
        {
            text.color = Color.Lerp(text.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}
