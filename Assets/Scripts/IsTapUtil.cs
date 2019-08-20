using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTapUtil : MonoBehaviour
{
    public static bool IsTapDown(){
        if (!Input.GetMouseButtonDown(0)) return false;

        return IsInGame();
    }
   public static bool IsTap(){
        if (!Input.GetMouseButton(0)) return false;

        
        return IsInGame();
    }

    public static bool IsInGame() {
        Debug.Log(Input.mousePosition.y);
            return Input.mousePosition.y <Screen.height*0.8;
    }
}
