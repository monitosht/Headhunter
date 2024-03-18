using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIni : MonoBehaviour
{
    public bool gunScene;
    public bool cameraShake;
    public bool refill;
    public bool fullHealth;

    private void Awake()
    {
        if(refill == true)
        {
            FindObjectOfType<PotionSystem>().Refill();
        }
        if(fullHealth == true)
        {
            FindObjectOfType<PlayerManager>().health = FindObjectOfType<PlayerManager>().maxHealth;
        }

    }
    private void Start()
    {
        UbhObjectPool.instance.RemoveAllPool();

        if (GameObject.FindGameObjectWithTag("PressE") != null)
        {
            GameObject.FindGameObjectWithTag("PressE").SetActive(false);
            Debug.Log("done");
        }
    }

    void Update()
    {
        if (gunScene == false)
        {
            FindObjectOfType<GunController>().gunScene = false;
        }
        else
        {
            FindObjectOfType<GunController>().gunScene = true;
        }

        if (cameraShake == true)
        {
            FindObjectOfType<CameraShake>().constantShake = true;
        }
        else
        {
            FindObjectOfType<CameraShake>().constantShake = false;
        }
    }
}
