using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string sceneName;
    private bool canEnter;
    private float timer = 2f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && canEnter == true)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = false;
        }
    }
}
