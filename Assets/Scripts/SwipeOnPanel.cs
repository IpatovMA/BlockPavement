using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeOnPanel : MonoBehaviour, IBeginDragHandler,IDragHandler 

{
    public float speed;
    public GameObject player;
    private static bool RightSwipe=false;
    private static bool UpSwipe=false;
    private static bool LeftSwipe=false;
    private static bool DownSwipe=false;

    public void OnBeginDrag(PointerEventData eventData)
    {   
        ResetSwipe();
        if((Mathf.Abs(eventData.delta.x))>(Mathf.Abs(eventData.delta.y))){
            if(eventData.delta.x>0){
                    RightSwipe=true;
            }else{
                     LeftSwipe=true;
            }
        }else{
            if(eventData.delta.y>0){
                    UpSwipe=true;
            }else{
                     DownSwipe=true;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    void Update()
    {   
        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
        if (velocity.magnitude>0.01f) {
            return;
        }
        if (Input.GetKeyDown ("w")||UpSwipe)
        {velocity = Vector2.up*speed;
                    Debug.Log("up");
        }
        if (Input.GetKeyDown ("s")||DownSwipe)
        {velocity = Vector2.down*speed;
                    Debug.Log("down");
        }
        if (Input.GetKeyDown ("a")||LeftSwipe)
        {velocity = Vector2.left*speed;
                    Debug.Log("left");
        }
        if (Input.GetKeyDown ("d")||RightSwipe)
        {velocity = Vector2.right*speed;
                            Debug.Log("rugth");
        }
        ResetSwipe();

        player.GetComponent<Rigidbody2D>().velocity = velocity;
    }
    void ResetSwipe(){
        RightSwipe=false;
        UpSwipe=false;
        LeftSwipe=false;
        DownSwipe=false;
    }
}


