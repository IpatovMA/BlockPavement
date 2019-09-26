using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{

    public void UpdateBorder(MapData Data){
        // transform.
        // transform.Find("OutsideBorder").transform.localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
        // transform.Find("collider").transform.localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);

        // GetComponentInChildren<SpriteRenderer>().size = new Vector2(Data.MapWidth,Data.MapHeight);
        // // transform.position = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);
         transform.localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
        transform.localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);


    }


}
