using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInsideBorders : MonoBehaviour
{
       public GameObject[] BorderPrefabs;
    //    private GameObject[] BorderBlocks = new GameObject[2];
       

    public void Set()
    {
            // BorderBlocks[0] = Instantiate(BorderPrefabs[0],Vector3.zero,Quaternion.identity, border);
            // BorderBlocks[1] = Instantiate(BorderPrefabs[0],Vector3.zero,Quaternion.identity, border);
            // BorderBlocks[2] = Instantiate(BorderPrefabs[1],Vector3.zero,Quaternion.identity, border);
            // BorderBlocks[3] = Instantiate(BorderPrefabs[1],Vector3.zero,Quaternion.identity, border);
            // for (int i = 4; i<8;i++){
            //     BorderBlocks[i] = Instantiate(BorderPrefabs[i-2],Vector3.zero,Quaternion.identity, border);
            // }
            // map = transform.Find("map").transform;
        Vector3 halfVec = new Vector3(0,0,0);
        Vector3 topVec = new Vector3(0,0.909f,0);
        Vector3 leftVec = new Vector3(0.909f,0,0);


            var Borders = new List<Transform>();

        Component[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var coll in colliders)
        {
           if(coll.gameObject.tag=="border"){
            //    if(coll.GetComponentInParent<SetOutlineBorder>()!=null) continue;
               Borders.Add(coll.transform);

           } 
        }

        foreach (var border in Borders)
        {
            Debug.Log(border.transform.parent.name);

             RaycastHit2D[] rays = new RaycastHit2D[4] ;
             rays[0]= Physics2D.Raycast(border.position+halfVec, new Vector3(0,1,0));
             rays[1]= Physics2D.Raycast(border.position+halfVec, new Vector3(1,0,0));
             rays[2]= Physics2D.Raycast(border.position+halfVec, new Vector3(0,-1,0));
             rays[3]= Physics2D.Raycast(border.position+halfVec, new Vector3(-1,0,0));
            // Debug.DrawRay(border.position+halfVec)
                        Debug.DrawRay(border.position+halfVec, new Vector3(0,1,0),Color.red,10);
                        Debug.DrawRay(border.position+halfVec, new Vector3(1,0,0),Color.red,10);
                        Debug.DrawRay(border.position+halfVec, new Vector3(0,-1,0),Color.red,10);
                        Debug.DrawRay(border.position+halfVec, new Vector3(-1,0,0),Color.red,10);

            int[] clears = new int[4] ;

        for (int i = 0;i<4;i++)
        {
                if (rays[i].collider != null&&rays[i].collider.tag == "clear"){
                    clears[i]=1;
                }else clears[i]=0;
               Debug.Log(clears[i]);
        }

        if(clears[0]==1){
            Instantiate(BorderPrefabs[0],leftVec,Quaternion.identity, border.parent);
        }
        if(clears[1]==1){
            Instantiate(BorderPrefabs[1],topVec,Quaternion.identity, border.parent);
        }
        if(clears[2]==1){
            Instantiate(BorderPrefabs[0],Vector3.zero,Quaternion.identity, border.parent);
        }
        if(clears[3]==1){
            Instantiate(BorderPrefabs[1],Vector3.zero,Quaternion.identity, border.parent);
        }
        }
    }


}
