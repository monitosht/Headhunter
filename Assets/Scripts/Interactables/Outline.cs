using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Material outline;
    public float thickness;
    public Color colour;
    private bool playerEnabled;
    public bool mouse;

    void Start()
    {
        outline = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        outline.SetFloat("_OutlineThickness", thickness);
        outline.SetVector("_OutlineColour", colour);

        if(playerEnabled == false)
        {
            outline.DisableKeyword("OUTLINE_ON");
        }
    }

    //--------------------------------------------------------------

    private void OnMouseOver()
    {
        if(playerEnabled == true && mouse == true)
        {
            outline.EnableKeyword("OUTLINE_ON");
        }
    }
    private void OnMouseExit()
    {
        if (playerEnabled == true == mouse == true)
        {
            outline.DisableKeyword("OUTLINE_ON");
        }
    }

    //--------------------------------------------------------------

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = true;

            if(mouse == false)
            {
                outline.EnableKeyword("OUTLINE_ON");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = false;

            if (mouse == false)
            {
                outline.DisableKeyword("OUTLINE_ON");
            }
        }
    }
}
