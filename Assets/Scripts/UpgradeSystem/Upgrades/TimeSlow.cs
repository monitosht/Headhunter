using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    public float slowAmount;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = slowAmount;
        }
        else
        {
            if(FindObjectOfType<PauseScreen>().paused == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
}
