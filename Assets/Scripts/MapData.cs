using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapData : MonoBehaviour
{
    public int TotalBlockCount;
    public int MapWidth;
    public int MapHeight;
    public Vector3 PlayerPos;
    public GameObject PlayerPrafab;


    void Start()
    {   
 
        LoadMap();


        
    }
    
    public void LoadMap(){

 
       TotalBlockCount=0;
        var clearBlocks = new List<Transform>();

        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
           if(coll.gameObject.tag=="clear"){
            //    TotalBlockCount++;
               clearBlocks.Add(coll.transform);

           } 
        }
        TotalBlockCount = clearBlocks.Count;

        MapWidth=-1;
        MapHeight=-1;

        RaycastHit2D[] WidthRays = Physics2D.RaycastAll(new Vector3(0,0.5f,0), Vector2.right);
        foreach (var ray in WidthRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapWidth++;
                Debug.DrawLine(new Vector3(0,0.5f,0), ray.point,Color.red,20);
            }    
        }
           
        RaycastHit2D[] HeightRays = Physics2D.RaycastAll(new Vector3(0.5f,0,0), Vector2.up);
        foreach (var ray in HeightRays)
        {
                if (ray.collider != null&&(ray.collider.tag == "border"||ray.collider.tag == "clear"))
            {   
                MapHeight++;
                // Debug.DrawLine(new Vector3(0.5f,0,0), ray.point,Color.red,20);
            }    
        }

        PlayerPos = clearBlocks[Random.Range(0, TotalBlockCount)].position  - new Vector3(0.5f,0.5f,0);

        Instantiate(PlayerPrafab,PlayerPos,Quaternion.identity);

    }
}
