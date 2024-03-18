using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float enrageThreshhold;
    public GameObject sprite;
    private EnemyFlash flash;
    public GameObject explosion;
    public Animator healthbarFlash;
    public GameObject Drop1;
    public GameObject Drop2;
    public GameObject credits;
    public int creditsDrop;
    public GameObject teleporter;

    PlayerManager playerManager;
    GunController gunController;
    GameObject player;

    void Update()
    {
        if (health <= 0)
        {
            UbhObjectPool.instance.RemoveAllPool();
            Death();
        }
    }
    void Start()
    {
        FindPlayer();
        flash = sprite.GetComponent<EnemyFlash>();
        health = maxHealth;
    }
    public void TakeDamage()
    {
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        healthbarFlash.SetTrigger("Quick");
        FindObjectOfType<HitStop>().Stop(0.025f);
        StartCoroutine(flash.WaitForSpawn());
        health -= gunController.damage;

        if (FindObjectOfType<DamageDealt>() != null)
        {
            FindObjectOfType<DamageDealt>().damageDealt += gunController.damage;
        }
        PlayerStats.damageDealt += gunController.damage;
    }
    void FindPlayer()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gunController = FindObjectOfType<GunController>();
        player = FindObjectOfType<GunController>().gameObject;
    }
    void Death()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("EnemyExplode");
        CameraShake cam = FindObjectOfType<CameraShake>();
        cam.SingleShake(10f, 0.1f, 0.2f);
        Destroy(gameObject);
        PlayerStats.bossesKilled++;

        for (int i = 0; i < creditsDrop; i++)
        {
            Instantiate(credits, transform.position, Quaternion.identity);
        }
        Instantiate(Drop1, (new Vector3(6, 0, 0)), Quaternion.identity);
        Instantiate(Drop2, (new Vector3(-6, 0, 0)), Quaternion.identity);
        Instantiate(teleporter, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
