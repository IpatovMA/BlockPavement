using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRnd : MonoBehaviour
{
    public Texture[] Tiles;

    void Start(){
        
       GetComponentInChildren<Renderer>().material.mainTexture = Tiles[Random.Range(0,Tiles.Length)];
    }
}