using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{

    public int selectedGun = 0;
    public GameObject activeGun;
    public Transform lastGun;

    void Start()
    {
        SelectWeapon();
    }
    void Update()
    {
        int previousSelectedGun = selectedGun;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedGun >= transform.childCount - 1)
            {
                selectedGun = 0;
                PlaySFX();
            }
            else
            {
                selectedGun++;
                PlaySFX();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedGun <= 0)
            {
                selectedGun = transform.childCount - 1;
                PlaySFX();
            }
            else
            {
                selectedGun--;
                PlaySFX();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedGun = 0;
            PlaySFX();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedGun = 1;
            PlaySFX();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedGun = 2;
            PlaySFX();
        }

        if (previousSelectedGun != selectedGun)
        {
            SelectWeapon();
        }

        //-----------------------------------------------------------------------------------

        NewActiveGun();
        lastGun = transform.GetChild(transform.childCount - 1);

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeGun = gameObject.transform.GetChild(i).gameObject;
            }
        }

        if (gameObject.transform.childCount > 3)
        {
            Destroy(activeGun);
        }
    }


    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if(i == selectedGun)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    void NewActiveGun()
    {
        if(activeGun == null)
        {
            SelectWeapon();
        }
    }

    public void NewGun()
    {
        activeGun.SetActive(false);
        lastGun.gameObject.SetActive(true);
    }

    public IEnumerator Pickup()
    {
        yield return new WaitForSeconds(0.1f);
        activeGun.SetActive(false);
        lastGun.gameObject.SetActive(true);
    }

    void PlaySFX()
    {
        if(transform.childCount > 1)
        {
            FindObjectOfType<AudioManager>().Play("WeaponSwap");
        }
    }
}
