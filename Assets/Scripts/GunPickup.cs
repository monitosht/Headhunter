using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private bool playerEnabled;
    private GameObject sprite;
    public GameObject notification;
    public float offset = 0.5f;

    private void Start()
    {
        sprite = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if(playerEnabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<GunManager>().enabled = true;
                tag = "Gun";
                gameObject.SetActive(false);

                Notification noti = Instantiate(notification).GetComponent<Notification>();
                noti.gun = GetComponent<GunManager>().gun;

                Destroy(GetComponent<BoxCollider2D>());
                Destroy(sprite.GetComponent<BoxCollider2D>());
                Destroy(sprite.GetComponent<Outline>());
                Destroy(this);

                GunSwitch gs;

                gs = FindObjectOfType<GunSwitch>();
                transform.parent = gs.gameObject.transform;

                FindObjectOfType<AudioManager>().Play("WeaponSwap");

                transform.localPosition = new Vector2(offset, 0);
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEnabled = false;
        }
    }
}

/*public Gun gun;

private void OnTriggerStay2D(Collider2D collision)
{
    if(collision.tag == "Player")
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FindObjectOfType<GunManager>().gameObject.GetComponent<GunManager>().gun = gun;
            FindObjectOfType<GunManager>().gameObject.GetComponent<GunManager>().StatCheck();
            Destroy(gameObject);
        }       
    }
}*/
