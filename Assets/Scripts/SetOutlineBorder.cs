using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{

    public void UpdateBorder(MapData Data){
        transform.Find("Quads").localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
        // transform.Find("fild").localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);
        SpriteRenderer[] Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spr in Sprites)
        {
            spr.size = new Vector2(Data.MapWidth,Data.MapHeight);
            spr.transform.localPosition = new Vector3(Data.MapWidth/2.0f,Data.MapHeight/2.0f,spr.transform.localPosition.z);
        }
        // GetComponentInChildren<SpriteRenderer>().size = new Vector2(Data.MapWidth,Data.MapHeight);
        // GetComponentInChildren<SpriteRenderer>().transform.localPosition = new Vector3(Data.MapWidth/2.0f,Data.MapHeight/2.0f,0);
        transform.localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);

    }


}
