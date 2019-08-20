﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SwipeControl : MonoBehaviour
{
    private static Vector3 fp;   //Первая позиция касания
    private static Vector3 lp;   //Последняя позиция касания
    public float dragDistance = 30;  //Минимальная дистанция для определения свайпа
    private static Touch touch;
    private static bool RightSwipe=false;
    private static bool UpSwipe=false;
    private static bool LeftSwipe=false;
    private static bool DownSwipe=false;


    void Start()
    {
 
    }

   void Update(){
    if (Input.touchCount == 0) return;

    if(RightSwipe||LeftSwipe||UpSwipe||DownSwipe){
    RightSwipe=false;
    UpSwipe=false;
    LeftSwipe=false;
    DownSwipe=false;
    }
 
    touch = Input.GetTouch(0);  //проверяем первое касание
    if (touch.phase == TouchPhase.Began)
    {
        fp = touch.position; 
        lp = touch.position;
    }
 
    if (touch.phase == TouchPhase.Moved) 
    {
        lp = touch.position;
    }
 
    if (touch.phase == TouchPhase.Ended) 
    {
        ResetFp();
    }
    
    

    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
    {//это перемещение
            //проверяем, перемещение было вертикальным или горизонтальным 
        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
            {   //Если горизонтальное движение больше, чем вертикальное движение ...
                if ((lp.x>fp.x))  //Если движение было вправо
                {   //Свайп вправо
                    RightSwipe=true;
                }
                else
                {   //Свайп влево
                    LeftSwipe=true; 
                }
            }
        else
        {   //Если вертикальное движение больше, чнм горизонтальное движение
                if (lp.y>fp.y)  //Если движение вверх
                {   //Свайп вверх
                    UpSwipe=true; 
                }
                else
                {   //Свайп вниз
                   DownSwipe =true;
                }
        }
            fp = lp; //ставим начальную позицию на новое место
    }
}
    public static void ResetFp(){
    if(RightSwipe||LeftSwipe||UpSwipe||DownSwipe){
    RightSwipe=false;
    UpSwipe=false;
    LeftSwipe=false;
    DownSwipe=false;
    }
        fp = touch.position; 
        lp = touch.position;
    }
    public static  bool GetRightSwipe(){
        return RightSwipe;
    }
    public static bool GetLeftSwipe(){
        return LeftSwipe;
    }
    public static bool GetUpSwipe(){
        return UpSwipe;
    }
    public static bool GetDownSwipe(){
        return DownSwipe;
    }
}
