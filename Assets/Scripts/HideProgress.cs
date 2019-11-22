using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideProgress : MonoBehaviour
{
    bool hidden;
    Animator anim;
    float lastInputTime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        hidden = true;
    }


    void Update()
    { 
        Debug.Log(hidden);
        if(Input.GetMouseButton(0)||Input.GetKeyDown ("w")||Input.GetKeyDown ("a")||Input.GetKeyDown ("s")||Input.GetKeyDown ("d")){
            lastInputTime = Time.fixedTime;
            if (hidden){
                Debug.Log("eee");
            anim.SetTrigger("show");
            hidden = false;
            }
        }
        if (Time.fixedTime - lastInputTime>2f){
            
            if (!hidden){
            anim.SetTrigger("hide");
            hidden = true;
            }

        }
        //  Debug.Log(Time.fixedTime + "  "+lastInputTime);
    }
}
