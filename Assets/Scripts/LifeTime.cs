using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTime;

    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
