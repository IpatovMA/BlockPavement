using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.CoreModule;

public class Recolorer : MonoBehaviour
{
    public string Name;
    public int PalettesNumber = 3;
    int PaletteNum;

    void Start()
    {
        Name = transform.name;
        PaletteNum = Random.Range(1,PalettesNumber+1);
        
        Renderer[] Renderers = GetComponentsInChildren<Renderer>();

        foreach (var rend in Renderers)
        {
            foreach (var mat in rend.materials)
            {
                string path = "Models/"+Name+"/Тех"+PaletteNum+"/"+mat.name;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
