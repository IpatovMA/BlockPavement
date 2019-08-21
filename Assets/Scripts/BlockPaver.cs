using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPaver : MonoBehaviour
{


    public int DoneBlockCount=0;
    public Material[] materials;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "clear"){
            coll.gameObject.tag =  "paved";
            coll.gameObject.GetComponent<MeshRenderer>().material = materials[1];
            DoneBlockCount++;
            // Debug.Log(DoneBlockCount);
        }

    }

     void ChangeMaterial(int id, Material myMat) { 
         MeshRenderer mr = GetComponent<MeshRenderer>();
          Material[] mats = mr.materials;
           if (id < mats.Length) mats[id] = myMat;
            mr.materials = mats;
            }

}
