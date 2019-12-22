using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.CoreModule;

public class Recolorer : MonoBehaviour
{
    public string Name;
    public int PalettesNumber ;
    int PaletteNum;
    [SerializeField]bool WrappedDiffuse  = true;  
    [SerializeField]bool CustomBright  = false;  
    [SerializeField] float Brightness= 1;
    [Space]
     [SerializeField]bool UpdateColor = false;

    void Start()
    {
       Recolor();

    }
    
    void Update(){
        if (UpdateColor){
            Recolor();
             UpdateColor= false;
        }
    }


    void Recolor(){
        // Debug.Log("qq");
        float bright = CustomBright?Brightness:LevelManager.TotalBrightness;
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

                if(ColorUtility.ToHtmlStringRGB(mat.color) == "969696"||UpdateColor) {mat.color = new Color (bright,bright,bright,1);} //+0.36f
                
                if(mat.name =="Gazon (Instance)"||mat.name =="bor (Instance)"||mat.name =="borc (Instance)"){
                    mat.shader = Shader.Find("Diffuse");
                    continue;}
                
                if(mat.name =="No Name (Instance)"||mat.name =="Material #2 (Instance)"){
                    mat.shader = Shader.Find("Diffuse");
                    mat.color = Color.gray;
                    continue;}
                if(WrappedDiffuse)
                        mat.shader = Shader.Find("Wrapped Diffuse");
                    else mat.shader = Shader.Find("Diffuse");
                string path = "Models/"+Name+"/"+PaletteNum+"/"+mat.name;
                
                        string sep = " (Instance)";
                        int i = path.IndexOf(sep);
                        if (i!=-1) path = path.Substring(0, i);
                var tex = Resources.Load(path) as Texture;
                    if(tex == null){continue;}

                mat.mainTexture = tex;

                 
            }
        }

    }

}

