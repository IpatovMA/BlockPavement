using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageData : MonoBehaviour
{
    public int TotalBlockCount;
    public int MapWidth;
    public int MapHeight;

    void Start()
    {   
        TotalBlockCount=0;
        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
           if(coll.gameObject.tag=="clear"){
               TotalBlockCount++;
           } 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
