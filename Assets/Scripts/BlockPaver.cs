using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPaver : MonoBehaviour
{


    public int DoneBlockCount=0;
    public GameObject[] Dust;
    public Material paverMaterial;
    private Transform Stage;
    void Start()
    {
        Stage = GetComponentInParent<LevelStageData>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.tag == "clear"){
            coll.gameObject.tag =  "paved";
            coll.gameObject.GetComponent<MeshRenderer>().material = paverMaterial;
            // Debug.Log(Dust[0].GetComponent<ParticleSystem>().startLifetime);
            Destroy(Instantiate(Dust[0],coll.transform.position,Quaternion.Euler(GetComponent<PlayerControl>().GetEulerToAlign()),Stage),Dust[0].GetComponentInChildren<ParticleSystem>().startLifetime*2);
            // Destroy(Instantiate(Dust[0],coll.transform.position,Quaternion.identity),Dust[0].GetComponent<ParticleSystem>().startLifetime*2);
            // coll.transform.Translate(0,0,-0.001f);
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
