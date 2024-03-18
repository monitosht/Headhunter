using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        matDefault = sr.material;
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(Flash(0.125f));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(FlashMaterial(0.125f));
        }*/
    }
    public IEnumerator Flash(float timeBetweenFlash)
    {
        sr.material.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(timeBetweenFlash);

        Debug.Log("flashing");
    }
    public IEnumerator FlashMaterial(float timeBetweenFlash)
    {
        sr.material = matWhite;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matDefault;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matWhite;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matDefault;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matWhite;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matDefault;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matWhite;
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.material = matDefault;
        yield return new WaitForSeconds(timeBetweenFlash);
    }
}
