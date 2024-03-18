using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mouse.x < playerScreenPoint.x)
        {
            //gunSprite.GetChild(0).transform.localScale = new Vector3(gunSprite.GetChild(0).transform.localScale.x, -scaleY, gunSprite.GetChild(0).transform.localScale.z);
            transform.position = new Vector2(-0.4f, transform.position.y);
            Debug.Log("left");
        }
        else
        {
            //gunSprite.GetChild(0).transform.localScale = new Vector3(gunSprite.GetChild(0).transform.localScale.x, scaleY, gunSprite.GetChild(0).transform.localScale.z);
            transform.position = new Vector2(0.4f, transform.position.y);
            Debug.Log("right");
        }
    }
}
