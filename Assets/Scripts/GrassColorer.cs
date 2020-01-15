using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassColorer : MonoBehaviour
{
    public static int GrassColorNum;
    public static int GradStage=0;

    public Color[] Colors;
    public int StepsNum;
     Color deltaColor;
    
    void Start()
    {
       
        // NextColor();
 deltaColor = (Colors[1]-Colors[0])/StepsNum;

    }

    public void UpdateGrass()
    {
       Debug.Log(GrassColorNum+"   "+GradStage);

      
        if (GrassColorNum==StepsNum ){
        //    NextColor();
                    // deltaColor = (Colors[1]-Colors[0])/StepsNum;
                    // GrassColorNum=0;
                    GradStage =1;
        }
        if(GrassColorNum==-1){
            GradStage =0;
            GrassColorNum=1;
        }

        GetComponent<MeshRenderer>().material.color = Colors[0]+deltaColor*GrassColorNum;
         if(GradStage == 0){
         GrassColorNum++;}
         else { GrassColorNum--;}

    }

    void NextColor(){
        GrassColorNum = 1;
        if (GradStage>=Colors.Length){
            GradStage=-1;
            deltaColor = (Colors[0]-Colors[Colors.Length-1])/StepsNum;
        }else{
            deltaColor = (Colors[GradStage+1]-Colors[GradStage])/StepsNum;
            }
        GradStage++;

    }
}
