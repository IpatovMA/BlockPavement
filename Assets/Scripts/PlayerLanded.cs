using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanded : MonoBehaviour
{

    public void Laning(){
        GetComponentInParent<BoxCollider2D>().enabled = true;
        transform.position = new Vector3(transform.position.x,transform.position.y,0);


    }
    public void ReadyToGo(){
        SwipeControl.AllowSwipeInput();
    }
}
