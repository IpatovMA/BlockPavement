using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {   
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (velocity.magnitude>0.01f) {
            // SwipeControl.ResetFp();
            Debug.Log("stoped");
            return;
        }
        if(velocity.magnitude==speed){velocity = Vector2.zero;}

        if (Input.GetKeyDown ("w")||SwipeControl.GetUpSwipe())
        {velocity = Vector2.up*speed;
                    Debug.Log("up");
        }
        if (Input.GetKeyDown ("s")||SwipeControl.GetDownSwipe())
        {velocity = Vector2.down*speed;
                    Debug.Log("down");
        }
        if (Input.GetKeyDown ("a")||SwipeControl.GetLeftSwipe())
        {velocity = Vector2.left*speed;
                    Debug.Log("left");
        }
        if (Input.GetKeyDown ("d")||SwipeControl.GetRightSwipe())
        {velocity = Vector2.right*speed;
                            Debug.Log("rigth");
        }
            SwipeControl.ResetFp();

        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
