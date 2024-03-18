using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : MonoBehaviour
{
    private GameObject player;
    public float offset;
    public float speed;
    public float waitTime;
    private bool active;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        float x = Random.Range(-1f, 1f) * offset;
        float y = Random.Range(-1f, 1f) * offset;

        Vector3 pos = transform.position += new Vector3(x, y);

        transform.position = pos;
    }
    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }
    void Update()
    {
        if(active == true)
        {
            if (player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(waitTime);
        active = true;
    }
}
