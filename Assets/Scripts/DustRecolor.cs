using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustRecolor : MonoBehaviour
{
    void Start()
    {
        var dust = GetComponentsInChildren<ParticleSystem>();
        foreach (var d in dust)
        {
            var m = d.main;
            m.startColor= GetComponentInParent<MapData>().DustColor;
        }
    }


}
