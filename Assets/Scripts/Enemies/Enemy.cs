using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public float speed, playerCheck;
    [HideInInspector] public bool canMove;
    public float health;
    bool follow;

    public float minValue, maxValue;
    private float dropValue;

    PlayerManager playerManager;
    GunController gunController;
    GameObject player;

    public GameObject explosionPS;
   
    [HideInInspector] public GameObject credits;
    [HideInInspector] public EnemyFlash flash;

    void Start()
    {
        dropValue = Random.Range(minValue, maxValue);
    }
    void Update()
    {
        FindPlayer();
        HealthCheck();
        //FollowPlayer();
    }
    public void TakeDamage()
    {
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        FindObjectOfType<HitStop>().Stop(0.025f);
        StartCoroutine(flash.WaitForSpawn());
        health -= gunController.damage;

        if (FindObjectOfType<DamageDealt>() != null)
        {
            FindObjectOfType<DamageDealt>().damageDealt += gunController.damage;
        }
        PlayerStats.damageDealt += gunController.damage;
    }
    void HealthCheck()
    {
        if (health <= 0)
        {
            //FindObjectOfType<ScreenFlash>().StartFlash(0.2f, 0.4f, Color.white);
            //Invoke("Death", 0.5f);

            Death();

            flash.gameObject.GetComponent<Animator>().Play("DeathAnim");
        }
    }
    void FindPlayer()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gunController = FindObjectOfType<GunController>();
        player = FindObjectOfType<GunController>().gameObject;
    }
    void FollowPlayer()
    {
        if(canMove == true)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < playerCheck)
            {
                follow = true;
            }
            if (follow == true)
            {
                if (Vector2.Distance(player.transform.position, transform.position) < (playerCheck * +4))
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
            }
        }
    }
    void Death()
    {
        Instantiate(explosionPS, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("EnemyExplode");
        CameraShake cam = FindObjectOfType<CameraShake>();
        cam.SingleShake(10f, 0.1f, 0.2f);

        PlayerStats.enemiesKilled++;

        for (int i = 0; i < dropValue; i++)
        {
            Instantiate(credits, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
