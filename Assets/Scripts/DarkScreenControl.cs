using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkScreenControl : MonoBehaviour
{
    public bool Dark;

    public void AllDark(int i){
        if(i == 1){
            Dark = true;
        }
    }
    public void NoneDark(int i){
        if(i == 2){
            Dark = false;
        }
    }
}
