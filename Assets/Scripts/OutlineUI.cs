using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OutlineUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material outline;
    private Material mat;
    public float thickness;
    public Color colour;

    void Start()
    {
        Image image = GetComponent<Image>();

        mat = Instantiate(image.material);

        mat.SetFloat("_OutlineThickness", thickness);
        mat.SetVector("_OutlineColour", colour);

        image.material = mat;

        //outline = GetComponent<Image>().material;
    }
    void Update()
    {

    }

    //--------------------------------------------------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        mat.EnableKeyword("OUTLINE_ON");
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonHover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mat.DisableKeyword("OUTLINE_ON");
    }
}
