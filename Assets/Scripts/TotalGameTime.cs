using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameTime : MonoBehaviour
{
    void Update()
    {
        PlayerStats.totalTime += Time.deltaTime;
    }
}
