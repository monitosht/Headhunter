using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int i)
    {
        switch (i)
        {
            case (0): //level 1
            default:
                SceneManager.LoadScene("HH_LEVEL01");
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (1):
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (2):
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (3):
                break;

            //-----------------------------------------------------------------------------------------------------------

            case (4):
                break;

        }
    }
}
