using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.CoreModule;

public class recolor : MonoBehaviour
{
    public Material[] mats1;
    public Material[] mats2;
    public Material[] mats3;

    void Start()
    {
        Material[][] mats = {mats1,mats2,mats3};
        GetComponent<Renderer>().materials = mats[Random.Range(0,3)];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
