using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public GameObject attack;
    private bool canShoot;

    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.inRange == true)
        {
            attack.GetComponent<UbhShotCtrl>().StartShotRoutine(1);
        }
        else
        {
            //attack.GetComponent<UbhShotCtrl>().StopShotRoutine();
        }
    }

    public void ShotSound()
    {
        FindObjectOfType<AudioManager>().Play("EnemyShot");
    }
}
