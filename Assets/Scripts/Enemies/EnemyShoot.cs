using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private EnemyAI enemyAI;

    [SerializeField] private bool canShoot;
    public GameObject projectile;

    public float startFireRate;
    private float fireRate;

    //rapid fire variables
    public float startDuration;
    private float duration;
    private float timeBetweenShots = 0.4f;

    [Header("Enemy Type")]
    public bool basic;
    public bool rapidFire;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = gameObject.GetComponent<EnemyAI>();
        fireRate = startFireRate;
        duration = startDuration;
    }

    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (basic == true)
        {
            if (enemyAI.inRange == true)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }

            if (canShoot == true)
            {
                if (fireRate <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
                    fireRate = startFireRate;
                }
                else
                {
                    fireRate -= Time.deltaTime;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------

        if (rapidFire == true)
        {
            if (enemyAI.inRange == true)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }

            if (fireRate <= 0)
            {
                if (canShoot == true)
                {
                    duration -= Time.deltaTime;

                    if (duration <= 0)
                    {
                        duration = startDuration;
                        fireRate = startFireRate;
                    }
                    else
                    {
                        if (timeBetweenShots <= 0)
                        {
                            Instantiate(projectile, transform.position, Quaternion.identity);
                            timeBetweenShots = 0.2f;
                        }
                        else
                        {
                            timeBetweenShots -= Time.deltaTime;
                        }
                    }
                }

            }
            else
            {
                fireRate -= Time.deltaTime;
            }
        }
    }
}
