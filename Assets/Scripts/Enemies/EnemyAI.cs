using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float playerRange;
    public float withinRange;
    public float nextWaypointDistance;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public bool aggrod;
    public bool inRange;
    private bool stopped;

    Seeker seeker;
    Rigidbody2D rb;

    void Awake()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InvokeRepeating("CheckDist", 0, 0.1f);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1, 1, 1);
        }
    }
    private void Update()
    {
        float dist = Vector2.Distance(rb.position, target.position);

        if (dist <= playerRange)
        {
            aggrod = true;
        }
        if (dist <= withinRange)
        {
            inRange = true;
            stopped = true;
        }
        else
        {
            inRange = false;
            stopped = false;
        }
    }
    void CheckDist()
    {
        if (aggrod == true)
        {
            InvokeRepeating("UpdatePath", 0, .5f);
        }
        else
        {
            CancelInvoke("UpdatePath");
            path = null;
        }

        /*if (aggrod == true && inRange == true)
        {
            CancelInvoke("UpdatePath");
            path = null;
        }
        else if (aggrod == true && inRange == false)
        {
            InvokeRepeating("UpdatePath", 0, .5f);
        }*/
    }
}
