using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private GunController gunController;
    private GameObject player;
    public Gun gun;
    private GunSwitch gunSwitch;

    private void Awake()
    {

    }
    void Start()
    {
        gunSwitch = FindObjectOfType<GunSwitch>();
        gunController = FindObjectOfType<GunController>();
        player = FindObjectOfType<PlayerController>().gameObject;

        StatCheck();
        StartCheck();
    }
    private void OnEnable()
    {
        StatCheck();
        EnableCheck();
    }

    private bool duActive;
    private bool rdActive;

    /*
     * private void Update()
    {
        if (FindObjectOfType<DamageUp>() != null && duActive == false)
        {
            gun.damage = (gun.damage *= FindObjectOfType<DamageUp>().damgeMultiplier);
            duActive = true;
        }


        if (FindObjectOfType<ReloadDown>() != null && rdActive == false)
        {
            gun.reloadTime = (gun.reloadTime -= FindObjectOfType<ReloadDown>().amount);
            rdActive = true;
        }
    }*/

    public void StatCheck()
    {
        gunController.damage = gun.damage;
        gunController.maxAmmo = gun.maxAmmo;
        gunController.startReloadTime = gun.reloadTime;
        gunController.ammoPerBullet = gun.ammoPerBullet;
        gunController.timeBetweenShots = gun.fireRate;
        gunController.bulletPrefab = gun.bulletPrefab;
        gunController.holdShoot = gun.holdShoot;
        gunController.deviation = gun.deviation;
        gunController.scaleY = gun.scaleY;

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = gun.sprite;
        transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(gun.scaleX, gun.scaleY, 0);
    }

    void StartCheck()
    {
        if(gunSwitch.selectedGun == 0)
        {
            AmmoManager.gun1CurrentAmmo = gun.currentAmmo;
            gunController.currentAmmo = AmmoManager.gun1CurrentAmmo;

            AmmoManager.gun1TotalAmmo = gun.totalAmmo;
            gunController.totalAmmo = AmmoManager.gun1TotalAmmo;
        }
        if (gunSwitch.selectedGun == 1)
        {
            AmmoManager.gun2CurrentAmmo = gun.currentAmmo;
            gunController.currentAmmo = AmmoManager.gun2CurrentAmmo;

            AmmoManager.gun2TotalAmmo = gun.totalAmmo;
            gunController.totalAmmo = AmmoManager.gun2TotalAmmo;
        }
        if (gunSwitch.selectedGun == 2)
        {
            AmmoManager.gun3CurrentAmmo = gun.currentAmmo;
            gunController.currentAmmo = AmmoManager.gun3CurrentAmmo;

            AmmoManager.gun3TotalAmmo = gun.totalAmmo;
            gunController.totalAmmo = AmmoManager.gun3TotalAmmo;
        }
    }

    void EnableCheck()
    {
        if(gunSwitch.selectedGun == 0)
        {
            gunController.currentAmmo = AmmoManager.gun1CurrentAmmo;
            gunController.totalAmmo = AmmoManager.gun1TotalAmmo;

            //Debug.Log(AmmoManager.gun1CurrentAmmo + "gun 1 current ammo set");
        }
        if (gunSwitch.selectedGun == 1)
        {
            gunController.currentAmmo = AmmoManager.gun2CurrentAmmo;
            gunController.totalAmmo = AmmoManager.gun2TotalAmmo;

            //Debug.Log(AmmoManager.gun2CurrentAmmo + "gun 1 current ammo set");
        }
        if (gunSwitch.selectedGun == 2)
        {
            gunController.currentAmmo = AmmoManager.gun3CurrentAmmo;
            gunController.totalAmmo = AmmoManager.gun3TotalAmmo;

            //Debug.Log(AmmoManager.gun3CurrentAmmo + "gun 1 current ammo set");
        }
    } 
}
