using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GUN00", menuName = "Items/Gun")]
public class Gun : ScriptableObject
{
    [Header("Name")]
    public string gunName;
    [TextArea(5, 15)]
    public string gunDescription;

    [Header("Stats")]
    //
    public float fireRate;
    public float damage;
    public float currentAmmo;
    public float maxAmmo;
    public float totalAmmo;
    public float ammoPerBullet;
    public float reloadTime;
    public float deviation;
    public bool holdShoot;

    [Header("Artwork")]
    //
    public GameObject bulletPrefab;
    public Sprite sprite;
    public float scaleX, scaleY;
}
