using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class ScreenFlash : MonoBehaviour
{
    Image image = null;
    Coroutine currentFlash = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFlash(float duration, float maxAlpha, Color colour)
    {
        image.color = colour;

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if(currentFlash != null)
        {
            StopCoroutine(currentFlash);
        }
        currentFlash = StartCoroutine(Flash(duration, maxAlpha));
    }

    IEnumerator Flash(float duration, float maxAlpha)
    {
        float flashIn = duration / 2;

        for (float t = 0; t < flashIn; t += Time.deltaTime)
        {
            Color tempColour = image.color;
            tempColour.a = Mathf.Lerp(0, maxAlpha, t / duration);
            image.color = tempColour;

            yield return null;
        }

        float flashOut = duration / 2;

        for (float t = 0; t < flashIn; t += Time.deltaTime)
        {
            Color tempColour = image.color;
            tempColour.a = Mathf.Lerp(maxAlpha, 0, t / duration);
            image.color = tempColour;

            yield return null;
        }

        image.color = new Color32(0, 0, 0, 0);
    }
}
