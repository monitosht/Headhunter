using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelFloor;
    public GameObject enemy;

    private GameObject[] enemies;
    private bool levelStarted;

    public GameObject astarGraph;
    private bool graphSpawned = false;


    public int enemyAmount;

    private void Awake()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.position = new Vector2(0, 0);
    }

    void Update()
    {
        levelFloor = GameObject.FindGameObjectsWithTag("LevelFloor");
        int rand = Random.Range(0, levelFloor.Length);

        if(enemyAmount > 0)
        {
            Instantiate(enemy, levelFloor[rand].transform.position, Quaternion.identity);
            enemyAmount--;
        }else if (enemyAmount == 0 && graphSpawned == false)
        {
            Instantiate(astarGraph, new Vector2(0, 0), Quaternion.identity);
            graphSpawned = true;
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies != null)
        {
            if (enemies.Length != 0)
            {
                levelStarted = true;
            }
        }
        if(levelStarted == true)
        {
            if (enemies.Length == 0)
            {
                Debug.Log("all dead");
            }
        }
    }
}
