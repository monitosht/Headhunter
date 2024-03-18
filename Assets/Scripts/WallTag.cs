using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTag : MonoBehaviour
{
    private bool done;
    public GameObject astar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(done == false)
        {
            GameObject generatedLevel;

            generatedLevel = GameObject.Find("Generated Level");

            GameObject tilemaps = generatedLevel.transform.Find("Tilemaps").gameObject;

            GameObject walls = tilemaps.transform.Find("Walls").gameObject;

            walls.tag = "Wall";
            walls.layer = 11;

            if(walls.tag == "Wall")
            {
                done = true;
            }
        }
        else
        {

        }

    }
}
