using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerOld : MonoBehaviour
{
    private GameObject gun;

    [HideInInspector]
    public float damage, currentAmmo, maxAmmo, totalAmmo, ammoPerBullet, startReloadTime, reloadTime;
    [HideInInspector]
    public Transform gunSprite, gunTip;
    [HideInInspector]
    public SpriteRenderer gunRend;
    [HideInInspector]
    public GameObject bulletPrefab;
    [HideInInspector]
    public float scaleY;
    [HideInInspector]
    public bool holdShoot;
    [HideInInspector]
    public float timeBetweenShots;
    [HideInInspector]
    public float deviation;

    private Transform guns;

    bool mouseLeft, canShoot, reloading;
    private float randomX, randomY;
    float lastShot;
    
    Vector3 mousePos, mouseVector;

    int playerSortingOrder = 20;
    private AudioSource source;
    CameraController Cam;

    private GunSwitch gunSwitch;

    void Start()
    {
        guns = gameObject.transform.GetChild(1);
        gun = GameObject.FindGameObjectWithTag("Gun");

        Cam = FindObjectOfType<CameraController>();
        source = GetComponent<AudioSource>();

        gunSwitch = FindObjectOfType<GunSwitch>();
    }    

    void Update()
    {
        Gun();
        Reload();
        Shooting();
        Animation();
        GetMouseInput();

        gun = GameObject.FindGameObjectWithTag("Gun");
    }

    void GetMouseInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        mouseVector = (mousePos - transform.position).normalized;
        if (holdShoot == true)
        {
            mouseLeft = Input.GetMouseButton(0);
        }
        else
        {
            mouseLeft = Input.GetMouseButtonDown(0);
        }
    }

    // GUN -------------------------------------------------------------------------------------------------------

    void Gun()
    {
        gunSprite = gun.transform;
        gunRend = gun.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        gunTip = gun.transform.GetChild(0).GetChild(0);
    }

    void Animation()
    {
        float gunAngle = -1 * Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg;
        gunSprite.rotation = Quaternion.AngleAxis(gunAngle, Vector3.back);
        gunRend.sortingOrder = playerSortingOrder - 1;

        if (gunAngle > 0)
        {
            gunRend.sortingOrder = playerSortingOrder + 1;
        }

        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mouse.x < playerScreenPoint.x)
        {
            gunSprite.GetChild(0).transform.localScale = new Vector3(gunSprite.GetChild(0).transform.localScale.x, -scaleY, gunSprite.GetChild(0).transform.localScale.z);
        }
        else
        {
            gunSprite.GetChild(0).transform.localScale = new Vector3(gunSprite.GetChild(0).transform.localScale.x, scaleY, gunSprite.GetChild(0).transform.localScale.z);
        }
    }

    void Shooting()
    {
        canShoot = (lastShot + timeBetweenShots < Time.time);

        if (mouseLeft && canShoot && reloading == false && currentAmmo > 0)
        {
            Vector3 spawnPos = gunTip.position;
            Quaternion spawnRot = Quaternion.identity;
            PlayerBullet bul = Instantiate(bulletPrefab, spawnPos, spawnRot).GetComponent<PlayerBullet>();
            bul.Setup(mouseVector += new Vector3(randomX, randomY, 0));
            lastShot = Time.time;
            Cam.Shake((transform.position - gunTip.position).normalized, 2f, 0.025f);
            UseAmmo();
        }

        randomX = Random.Range(-deviation, deviation);
        randomY = Random.Range(-deviation, deviation);
    }

    // RELOADING -------------------------------------------------------------------------------------------------------

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && (totalAmmo > 0) && reloading == false)
        {
            if (totalAmmo > (maxAmmo - currentAmmo) && ((maxAmmo - currentAmmo) != 0))
            {
                totalAmmo -= (maxAmmo - currentAmmo);
                currentAmmo = maxAmmo;

                reloadTime = startReloadTime;
                source.Play();
            }
            else if (totalAmmo > 0 && ((maxAmmo - currentAmmo) != 0))
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;

                reloadTime = startReloadTime;
                source.Play();
            }
        }

        if (currentAmmo == 0 && (totalAmmo > 0) && reloading == false)
        {
            if (totalAmmo > (maxAmmo - currentAmmo) && ((maxAmmo - currentAmmo) != 0))
            {
                totalAmmo -= (maxAmmo - currentAmmo);
                currentAmmo = maxAmmo;

                reloadTime = startReloadTime;
                source.Play();
            }
            else if (totalAmmo > 0 && ((maxAmmo - currentAmmo) != 0))
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;

                reloadTime = startReloadTime;
                source.Play();
            }
        }

        if (reloadTime <= 0)
        {
            reloading = false;
        }
        else
        {
            reloading = true;
            reloadTime -= Time.deltaTime;
        }
    }
    void UseAmmo()
    {
        //currentAmmo -= ammoPerBullet;
        AmmoManager.gun1CurrentAmmo -= ammoPerBullet;
    }

    /*private void OnEnable()
    {
        if(gunSwitch.selectedGun == 0)
        {
            currentAmmo = AmmoManager.gun1CurrentAmmo;
            totalAmmo = AmmoManager.gun1TotalAmmo;
        }
        if (gunSwitch.selectedGun == 1)
        {
            currentAmmo = AmmoManager.gun2CurrentAmmo;
            totalAmmo = AmmoManager.gun2TotalAmmo;
        }
        if (gunSwitch.selectedGun == 2)
        {
            currentAmmo = AmmoManager.gun3CurrentAmmo;
            totalAmmo = AmmoManager.gun3TotalAmmo;
        }
    } */
    // UI -------------------------------------------------------------------------------------------------------
}
