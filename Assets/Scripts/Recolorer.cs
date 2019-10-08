using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.CoreModule;

public class Recolorer : MonoBehaviour
{
    public string Name;
    public int PalettesNumber ;
    int PaletteNum;

    void Start()
    {
        int numSep = transform.name.LastIndexOf('_')+1;
        int spaceSep = transform.name.IndexOf(' ');
        if(spaceSep == -1){
        PalettesNumber = int.Parse(transform.name.Substring(numSep));
        Name = transform.name;

        }else{
        PalettesNumber = int.Parse(transform.name.Substring(numSep,spaceSep-numSep));
        Name = transform.name.Substring(0,spaceSep);
        }
        PaletteNum = Random.Range(1,PalettesNumber+1);
        
        Renderer[] Renderers = GetComponentsInChildren<Renderer>();

        foreach (var rend in Renderers)
        {
            foreach (var mat in rend.materials)
            {
                if(mat.name =="No Name (Instance)"){
                    mat.color = Color.gray;
                    continue;}
                string path = "Models/"+Name+"/"+PaletteNum+"/"+mat.name;
                
                        string sep = " (Instance)";
                        int i = path.IndexOf(sep);
                        if (i!=-1) path = path.Substring(0, i);
                         Debug.Log(path);
                var tex = Resources.Load(path) as Texture;
                // Assets/Resources/Models/Cube/Тех1/center.jpg
                // Debug.Log("Models/"+Name+"/Tex"+PaletteNum+"/"+mat.name);\
                Debug.Log(tex);
                mat.mainTexture = tex;
                mat.shader = Shader.Find("Diffuse");
                // mat.SetTexture("_MainTex", Resources.Load("Models/"+Name+"/Tex"+PaletteNum+"/"+mat.name) as Texture2D);
            }
        }

        Debug.Log("recolored");
        // GetComponentsInChildren<Renderer>().materials 

    }


}


    // Transform[] spawnPoints;

    // Transform[] ChooseSet (int numRequired) {
    //     Transform[] result = new Transform[numRequired];

    //     int numToChoose = numRequired;

    //     for (int numLeft = spawnPoints.Length; numLeft > 0; numLeft--) {

    //         float prob = (float)numToChoose/(float)numLeft;

    //         if (Random.value <= prob) {
    //             numToChoose--;
    //             result[numToChoose] = spawnPoints[numLeft - 1];

    //             if (numToChoose == 0) {
    //                 break;
    //             }
    //         }
    //     }
    //     return result;
    // }