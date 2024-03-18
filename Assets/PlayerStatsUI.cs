using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStatsUI : MonoBehaviour
{
    public TextMeshProUGUI totalGameTime;
    public TextMeshProUGUI damageDealt;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI bossesKilled;
    public TextMeshProUGUI damageTaken;
    public TextMeshProUGUI healingDone;
    public TextMeshProUGUI bulletsFired;
    public TextMeshProUGUI roomsCleared;

    void Start()
    {
        
    }
    void Update()
    {
        var ts = TimeSpan.FromSeconds(PlayerStats.totalTime);
        totalGameTime.text = "Game Time: " + string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        damageDealt.text = "Damage Dealt: " + PlayerStats.damageDealt.ToString("N0");
        enemiesKilled.text = "Enemies Killed: " + PlayerStats.enemiesKilled.ToString();
        bossesKilled.text =  "Bosses Killed: " + PlayerStats.bossesKilled.ToString();
        damageTaken.text = "Damage Taken: " + PlayerStats.damageTaken.ToString();
        healingDone.text = "Healing Done: " + PlayerStats.healingDone.ToString();
        bulletsFired.text = "Bullets Fired: " + PlayerStats.bulletsFired.ToString();
        roomsCleared.text = "Total Deaths: " + PlayerStats.totalDeaths.ToString();
    }
}
