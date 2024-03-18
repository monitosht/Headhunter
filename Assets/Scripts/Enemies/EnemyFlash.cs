using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    //public Enemy enemy;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        matDefault = sr.material;
    }
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    public IEnumerator WaitForSpawn()
    {
        while (Time.timeScale != 1)
            yield return null;
        sr.material = matWhite;
        Invoke("ResetMaterial", 0.1f);
    }
}
