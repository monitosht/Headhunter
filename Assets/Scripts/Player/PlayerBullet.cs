using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public bool decay;
    public float speed, speedDecay = 2f, minSpeed = 0.1f, startSpeed = 20, lifeTime;
    public Color particleColour;
    Vector3 dir, nextDir;
    public GameObject impactParticle;

    public void Setup(Vector3 _dir)
    {
        dir = _dir; //passed in from player
        speed = startSpeed; //start moving
    }
    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        //ParticleSystem ps = impactParticle.GetComponent<ParticleSystem>();
        //ParticleSystem.MainModule ma = ps.main;
        // ma.startColor = particleColour;
        //impactParticle.GetComponent<ParticleSystem>().startColor = particleColour;
    }

    void FixedUpdate()
    {
        Move(); //move the bullet
        CheckDisappear(); //get rid of bullet after it stops moving
    }

    void Move()
    {
        if(decay == true)
        {
            speed -= speedDecay * speed * Time.fixedDeltaTime; //slow down the bullet over time
        }
        if (speed < minSpeed)
        {
            speed = 0; //clamp down speed so it doesnt take too long to stop
        }
        Vector3 tempPos = transform.position; //capture current position
        tempPos += dir * speed * Time.fixedDeltaTime; //find new position
        transform.position = tempPos; //update position
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        impactParticle.GetComponent<ParticleSystem>().startColor = particleColour;

        if (other.gameObject.CompareTag("Wall"))
        {
            //impactParticle.GetComponent<ParticleSystem>().startColor = particleColour;

            speed = 0; //stop if it hits a wall
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            //impactParticle.GetComponent<ParticleSystem>().startColor = Color.white;


            other.gameObject.GetComponent<Enemy>().TakeDamage();
            speed = 0;
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            ///impactParticle.GetComponent<ParticleSystem>().startColor = Color.white;


            other.gameObject.GetComponent<Boss>().TakeDamage();
            speed = 0;
        }

        Instantiate(impactParticle, transform.position, Quaternion.identity);
    }

    void CheckDisappear()
    {
        if (speed == 0)//disappear and destroy when stopped
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
