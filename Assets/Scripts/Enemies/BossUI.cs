using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Slider healthbar;
    public Boss boss;

    void Update()
    {
        healthbar.maxValue = boss.maxHealth;
        healthbar.value = boss.health;
    }
}
