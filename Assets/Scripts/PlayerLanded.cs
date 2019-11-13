using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanded : MonoBehaviour
{

    public void Laning(){
        GetComponentInParent<BoxCollider2D>().enabled = true;
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,0);
        SwipeControl.AllowSwipeInput();

    }
    public void ReadyToGo(){
    }
}
