using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMaker : MonoBehaviour
{   
    public float height = 0.5f;
    public float width = 0.5f;
    public float factor = 2/3f;


   public void UpdateBushes(MapData Data){
       float t = height; //высота забора
       float m = width; // толщина забора
       float c = factor; // скос
  var s = new SimpleMeshBuilder();
    float h =  Data.MapHeight+0.2f;
    float w =Data.MapWidth+0.2f;
//   transform.eulerAngles = new Vector3(0,0,MapData.RotateOn%2 == 0?0:90);
  Debug.Log(MapData.RotateOn);
  // внутрення стенка
     s.AddRectSurface(  new Vector3(-w/2f, -h/2f, 0),new Vector3(-w/2f, -h/2f,-t), new Vector3(-w/2f, 0, -t*c), new Vector3(-w/2f,0, 0));

            s.AddConnectedEdge(new Vector3(-w/2f, 0, -t*c), new Vector3(-w/2f,0, 0));
        s.AddConnectedEdge(new Vector3(-w/2f, h/2f, -t), new Vector3(-w/2f, h/2f, 0));
            s.AddConnectedEdge(new Vector3(0, h/2f, -t*c), new Vector3(0, h/2f, 0));

        s.AddConnectedEdge(new Vector3(w/2f, h/2f, -t), new Vector3(w/2f, h/2f, 0));
            s.AddConnectedEdge(new Vector3(w/2f, 0, -t*c), new Vector3(w/2f, 0, 0));
        s.AddConnectedEdge(new Vector3(w/2f,-h/2f, -t), new Vector3(w/2f, -h/2f, 0));
            s.AddConnectedEdge(new Vector3(0,-h/2f, -t*c), new Vector3(0, -h/2f, 0));
        s.AddConnectedEdge(new Vector3(-w/2f, -h/2f, -t), new Vector3(-w/2f, -h/2f, 0));
      
      //внешняя стенка
       w++;h++;
       
        s.AddRectSurface(new Vector3(-w/2f, -h/2f, -t), new Vector3(-w/2f, -h/2f, 0), new Vector3(-w/2f, -h/2f+m,0), new Vector3(-w/2f, -h/2f+m, -t));

       
            s.AddConnectedEdge(new Vector3(-w/2f, 0, 0), new Vector3(-w/2f, 0, -t*c));
            s.AddConnectedEdge(new Vector3(-w/2f, h/2f-m, 0), new Vector3(-w/2f, h/2f-m, -t));

        s.AddConnectedEdge(new Vector3(-w/2f, h/2f, 0), new Vector3(-w/2f, h/2f, -t));

            s.AddConnectedEdge(new Vector3(-w/2f+m, h/2f, 0), new Vector3(-w/2f+m, h/2f, -t));
             s.AddConnectedEdge(new Vector3(0, h/2f, 0), new Vector3(0, h/2f, -t*c));
            s.AddConnectedEdge(new Vector3(w/2f-m, h/2f, 0), new Vector3(w/2f-m, h/2f, -t));

        s.AddConnectedEdge(new Vector3(w/2f, h/2f, 0), new Vector3(w/2f, h/2f, -t));

        s.AddConnectedEdge(new Vector3(w/2f, h/2f-m, 0), new Vector3(w/2f, h/2f-m, -t));
            s.AddConnectedEdge(new Vector3(w/2f, 0, 0), new Vector3(w/2f, 0,-t*c));
        s.AddConnectedEdge(new Vector3(w/2f,-h/2f+m, 0), new Vector3(w/2f, -h/2f+m, -t));

         s.AddConnectedEdge(new Vector3(w/2f,-h/2f, 0), new Vector3(w/2f, -h/2f, -t));

             s.AddConnectedEdge(new Vector3(w/2f-m,-h/2f, 0), new Vector3(w/2f-m, -h/2f, -t));
             s.AddConnectedEdge(new Vector3(0,-h/2f, 0), new Vector3(0, -h/2f, -t*c));
            s.AddConnectedEdge(new Vector3(-w/2f+m, -h/2f, 0), new Vector3(-w/2f+m, -h/2f, -t));

        s.AddConnectedEdge(new Vector3(-w/2f,-h/2f, 0), new Vector3(-w/2f, -h/2f, -t));

         //верхняя часть
            //низ
          s.AddRectSurface(new Vector3(-w/2f, -h/2f, -t), new Vector3(-w/2f, -h/2f+m, -t), new Vector3(-w/2f+m, -h/2f+m,-t), new Vector3(-w/2f+m, -h/2f, -t));

            s.AddConnectedEdge(new Vector3(0,-h/2f+m, -t*c), new Vector3(0, -h/2f, -t*c));
            s.AddConnectedEdge(new Vector3(w/2f-m,-h/2f+m, -t), new Vector3(w/2f-m, -h/2f, -t));
            s.AddConnectedEdge(new Vector3(w/2f,-h/2f+m, -t), new Vector3(w/2f, -h/2f, -t));
        //     //верх
           s.AddRectSurface(new Vector3(w/2f, h/2f, -t), new Vector3(w/2f, h/2f-m, -t), new Vector3(w/2f-m, h/2f-m,-t), new Vector3(w/2f-m, h/2f, -t));
            s.AddConnectedEdge(new Vector3(0,h/2f-m, -t*c), new Vector3(0, h/2f, -t*c));
             s.AddConnectedEdge(new Vector3(-w/2f+m,h/2f-m, -t), new Vector3(-w/2f+m, h/2f, -t));
            s.AddConnectedEdge(new Vector3(-w/2f,h/2f-m, -t), new Vector3(-w/2f, h/2f, -t));
        //     //лево
           s.AddRectSurface(new Vector3(-w/2f+m, -h/2f+m, -t), new Vector3(-w/2f, -h/2f+m, -t), new Vector3(-w/2f, 0,-t*c), new Vector3(-w/2f+m, 0, -t*c));
             s.AddConnectedEdge(new Vector3(-w/2f,h/2f-m, -t), new Vector3(-w/2f+m, h/2f-m, -t));
     //     //право
           s.AddRectSurface(new Vector3(w/2f, -h/2f+m, -t), new Vector3(w/2f-m, -h/2f+m, -t), new Vector3(w/2f-m, 0,-t*c), new Vector3(w/2f, 0, -t*c));
             s.AddConnectedEdge(new Vector3(w/2f-m,h/2f-m, -t), new Vector3(w/2f, h/2f-m, -t));

        GetComponent<MeshFilter>().mesh = s.ToMesh();
    }
    
}
