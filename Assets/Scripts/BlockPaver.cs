using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPaver : MonoBehaviour
{

    public int TotalBlockCount;
    public int DoneBlockCount=0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(TotalBlockCount==DoneBlockCount){
            Debug.Log("Level Complete!");
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "clear"){
            coll.gameObject.tag =  "paved";
            coll.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.72f, 0.2f, 0.2f, 1);
            DoneBlockCount++;
            // Debug.Log(DoneBlockCount);
        }

    }

}
