using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossTime : MonoBehaviour
{
    public TextMeshProUGUI totalGameTime;

    void Update()
    {
        var ts = TimeSpan.FromSeconds(PlayerStats.totalTime);
        totalGameTime.text = "Boss Completion Time: " + string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
}
