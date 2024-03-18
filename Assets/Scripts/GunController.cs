using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
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

    [SerializeField]
    private float camMag = 3f, camLength = 0.02f;

    private Transform guns;

    bool mouseLeft, canShoot, reloading;
    private float randomX, randomY;
    float lastShot;
    
    Vector3 mousePos, mouseVector;

    int playerSortingOrder = 20;
    private AudioSource source;
    CameraController Cam;

    private GunSwitch gunSwitch;
    private PlayerController playerController;

    public Transform aim;

    public bool gunScene = true;

    void Start()
    {
        guns = gameObject.transform.GetChild(1);
        gun = GameObject.FindGameObjectWithTag("Gun");
        playerController = FindObjectOfType<PlayerController>();

        Cam = FindObjectOfType<CameraController>();
        source = GetComponent<AudioSource>();

        gunSwitch = FindObjectOfType<GunSwitch>();
    }    

    void Update()
    {
        if(gunScene == true)
        {
            gunSwitch.gameObject.SetActive(true);

            if (playerController.rolling == false)
            {
                GetMouseInput();

                gun = GameObject.FindGameObjectWithTag("Gun");

                Gun();
                Reload();
                Shooting();
                Animation();
                CurrentGun();


                if (FindObjectOfType<Sharpshooter>() != null)
                {
                    Sharpshooter();
                }
            }
        }
        else
        {
            gunSwitch.gameObject.SetActive(false);
        }

       // Debug.Log(deviation);
       // Debug.Log(damage);
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
        aim.rotation = Quaternion.AngleAxis(gunAngle, Vector3.back);
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
            //Quaternion spawnRot = Quaternion.identity;
            PlayerBullet bul = Instantiate(bulletPrefab, spawnPos, aim.rotation).GetComponent<PlayerBullet>();
            bul.Setup(mouseVector += new Vector3(randomX, randomY, 0));
            lastShot = Time.time;
            Cam.Shake((transform.position - gunTip.position).normalized, camMag, camLength);
            UseAmmo();
            FindObjectOfType<AudioManager>().Play("Shoot");
            PlayerStats.bulletsFired++;
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
                FindObjectOfType<AudioManager>().Play("Reload");
            }
            else if (totalAmmo > 0 && ((maxAmmo - currentAmmo) != 0))
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;

                reloadTime = startReloadTime;
                FindObjectOfType<AudioManager>().Play("Reload");
            }
        }

        if (currentAmmo == 0 && (totalAmmo > 0) && reloading == false)
        {
            if (totalAmmo > (maxAmmo - currentAmmo) && ((maxAmmo - currentAmmo) != 0))
            {
                totalAmmo -= (maxAmmo - currentAmmo);
                currentAmmo = maxAmmo;

                reloadTime = startReloadTime;
                FindObjectOfType<AudioManager>().Play("Reload");
            }
            else if (totalAmmo > 0 && ((maxAmmo - currentAmmo) != 0))
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;

                reloadTime = startReloadTime;
                FindObjectOfType<AudioManager>().Play("Reload");
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
        if(FindObjectOfType<Thrifty>() != null)
        {
            float percent = Random.Range(0, 100);
            if(percent > FindObjectOfType<Thrifty>().chance)
            {
                currentAmmo -= ammoPerBullet;
            }
            else
            {
                currentAmmo -= 0;
            }

        }
        else
        {
            currentAmmo -= ammoPerBullet;
        }
    }

    void CurrentGun()
    {
        if(gunSwitch.selectedGun == 0)
        {
            //currentAmmo = AmmoManager.gun1CurrentAmmo;
            //totalAmmo = AmmoManager.gun1TotalAmmo;

            AmmoManager.gun1CurrentAmmo = currentAmmo;
            totalAmmo = Mathf.Infinity;

            //Debug.Log("Gun 1 Ammo = " + AmmoManager.gun1CurrentAmmo + " / " + AmmoManager.gun1TotalAmmo);
        }
        if (gunSwitch.selectedGun == 1)
        {
            //currentAmmo = AmmoManager.gun2CurrentAmmo;
            //totalAmmo = AmmoManager.gun2TotalAmmo;

            AmmoManager.gun2CurrentAmmo = currentAmmo;
            AmmoManager.gun2TotalAmmo = totalAmmo;

            //Debug.Log("Gun 2 Ammo = " + AmmoManager.gun2CurrentAmmo + " / " + AmmoManager.gun2TotalAmmo);
        }
        if (gunSwitch.selectedGun == 2)
        {
            //currentAmmo = AmmoManager.gun3CurrentAmmo;
            //totalAmmo = AmmoManager.gun3TotalAmmo;

            AmmoManager.gun3CurrentAmmo = currentAmmo;
            AmmoManager.gun3TotalAmmo = totalAmmo;

            //Debug.Log("Gun 3 Ammo = " + AmmoManager.gun3CurrentAmmo + " / " + AmmoManager.gun3TotalAmmo);
        }
    }

    private bool ssActive = false;

    void Sharpshooter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(ssActive == false)
            {
                deviation -= FindObjectOfType<Sharpshooter>().deviationAmount;

                damage *= FindObjectOfType<Sharpshooter>().damageAmount;

                ssActive = true;
            }
        }
        else
        {
            if(ssActive == true)
            {
                deviation += FindObjectOfType<Sharpshooter>().deviationAmount;

                damage /= FindObjectOfType<Sharpshooter>().damageAmount;

                ssActive = false;
            }
        }
    }
}
