using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SwipeControl : MonoBehaviour
{
    public GameObject player;  
    public float speed;
    public float dragDistance = 30;  //Минимальная дистанция для определения свайпа
    private static Vector3 fp;   //Первая позиция касания
    private static Vector3 lp;   //Последняя позиция касания
    private static Touch touch;
    private static bool RightSwipe=false;
    private static bool UpSwipe=false;
    private static bool LeftSwipe=false;
    private static bool DownSwipe=false;
    private static bool AllowSwipes = true;
     enum SwipeDir {UP,DOWN,LEFT,RIGHT,ZERO};

    
    void Start()
    {
 
    }

   void Update(){
    // if (Input.touchCount == 0||!AllowSwipes) {
    //     Debug.Log("Blocked");
    //     ResetFp();
    //     return;}

  //  if(RightSwipe||LeftSwipe||UpSwipe||DownSwipe){
    RightSwipe=false;
    UpSwipe=false;
    LeftSwipe=false;
    DownSwipe=false;

    SwipeDir Dir=SwipeDir.ZERO;
   // }
    if(Input.touchCount>0&&AllowSwipes) {
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
            BlockSwipeInput();
                //проверяем, перемещение было вертикальным или горизонтальным 
            if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                {   //Если горизонтальное движение больше, чем вертикальное движение ...
                    if ((lp.x>fp.x))  //Если движение было вправо
                    {   //Свайп вправо
                        RightSwipe=true;
                        Dir = SwipeDir.RIGHT;
                    }
                    else
                    {   //Свайп влево
                        LeftSwipe=true; 
                        Dir = SwipeDir.LEFT;
                    }
                }
            else
            {   //Если вертикальное движение больше, чнм горизонтальное движение
                    if (lp.y>fp.y)  //Если движение вверх
                    {   //Свайп вверх
                        UpSwipe=true; 
                        Dir = SwipeDir.UP;
                    }
                    else
                    {   //Свайп вниз
                    DownSwipe =true;
                        Dir = SwipeDir.DOWN;
                        
                    }
            }
                SetPlayerVelocity(Dir);
                fp = lp; //ставим начальную позицию на новое место
                // BlockSwipeInput();
        }
    
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
    
    void SetPlayerVelocity(SwipeDir dir){
         Vector2 velocity;
        switch(dir){
            case SwipeDir.UP:velocity = Vector2.up*speed;
            break;
            case SwipeDir.DOWN:velocity = Vector2.down*speed;
            break;
            case SwipeDir.LEFT:velocity = Vector2.left*speed;
            break;
            case SwipeDir.RIGHT:velocity = Vector2.right*speed;
            break; 
            default:  velocity = Vector2.zero; break;
        }
        player.GetComponent<Rigidbody2D>().velocity = velocity;

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
    public static void BlockSwipeInput(){
        AllowSwipes = false;
    }
    public static void AllowSwipeInput(){
        AllowSwipes = true;
    }
}
