using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSkip : MonoBehaviour
{
    public float timer;

    void Awake()
    {
        Invoke("LoadBossOver", timer);
    }

    public void LoadBossOver()
    {
        SceneManager.LoadScene("HH_BOSSOVER");
    }
}
