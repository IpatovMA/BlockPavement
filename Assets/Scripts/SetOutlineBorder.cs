using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{
    public int uzorNum = 6;
    public int palettesNum=6;
    public float scaleUzor = 1f;
    private Transform scaler;
    private Transform border;
    public GameObject[] BorderPrefabs;
    int currentUzor;
    int currentPalette;
    CustomOrnamentFile OrnamentData;
    private GameObject[] BorderBlocks = new GameObject[8];

    void Start(){

        TextAsset JsonData=(TextAsset)Resources.Load("Configs/ornaments"); 
            string json=JsonData.text;
            OrnamentData = JsonUtility.FromJson<CustomOrnamentFile>(json);

            scaler =transform.Find("scaler").transform;
            border = transform.Find("border").transform;

            BorderBlocks[0] = Instantiate(BorderPrefabs[0],Vector3.zero,Quaternion.identity, border);
            BorderBlocks[1] = Instantiate(BorderPrefabs[0],Vector3.zero,Quaternion.identity, border);
            BorderBlocks[2] = Instantiate(BorderPrefabs[1],Vector3.zero,Quaternion.identity, border);
            BorderBlocks[3] = Instantiate(BorderPrefabs[1],Vector3.zero,Quaternion.identity, border);
            for (int i = 4; i<8;i++){
                BorderBlocks[i] = Instantiate(BorderPrefabs[i-2],Vector3.zero,Quaternion.identity, border);
            }
    }

    public void UpdateBorder(MapData Data){
        
        scaler.localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
        // transform.Find("collider").localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);

        currentUzor = Random.Range(1,uzorNum+1);
        currentPalette = Random.Range(0,palettesNum);

        MeshRenderer uzor = scaler.Find("uzor").GetComponent<MeshRenderer>();
            
            string path = "Ornament/Ornament"+currentUzor;
                var tex = Resources.Load(path) as Texture;
            uzor.material.mainTexture = tex;

            Color tempColor = new Color();
            ColorUtility.TryParseHtmlString(OrnamentData.Ornaments[currentPalette].UzorColor, out tempColor);
            uzor.material.color = tempColor;
            uzor.material.mainTextureScale = new Vector2(Data.MapWidth*scaleUzor,Data.MapHeight*scaleUzor);
        
        ColorUtility.TryParseHtmlString(OrnamentData.Ornaments[currentPalette].BackColor, out tempColor);
        scaler.Find("back").GetComponent<MeshRenderer>().material.color = tempColor;


   SetBorderBlocks(Data);

    }

    void SetBorderBlocks(MapData Data){
        int Width = Data.MapWidth;
        int Height = Data.MapHeight;

        BorderBlocks[0].transform.localPosition = new Vector3(0,1,0);
            BorderBlocks[0].transform.localScale = new Vector3(1,Height-2,1);
            // ResizeBorderTex(BorderBlocks[0],Height);
            BorderBlocks[0].GetComponentInChildren<MeshRenderer>().material.mainTextureScale = new Vector2(1,Height-2);
        BorderBlocks[1].transform.localPosition = new Vector3(Width,1,0);
            BorderBlocks[1].transform.localScale = new Vector3(-1,Height-2,1);
            // ResizeBorderTex(BorderBlocks[1],Height);

            BorderBlocks[1].GetComponentInChildren<MeshRenderer>().material.mainTextureScale = new Vector2(1,Height-2);
        BorderBlocks[2].transform.localPosition = new Vector3(1,0,0);
            BorderBlocks[2].transform.localScale = new Vector3(Width-2,1,1);
            // ResizeBorderTex(BorderBlocks[2],Width);

            BorderBlocks[2].GetComponentInChildren<MeshRenderer>().material.mainTextureScale = new Vector2(1,Width-2);
        BorderBlocks[3].transform.localPosition = new Vector3(1,Height,0);
            BorderBlocks[3].transform.localScale = new Vector3(Width-2,-1,1);
            // ResizeBorderTex(BorderBlocks[3],Width);

            BorderBlocks[3].GetComponentInChildren<MeshRenderer>().material.mainTextureScale = new Vector2(1,Width-2);
        BorderBlocks[4].transform.localPosition = new Vector3(0,0,0);
        BorderBlocks[5].transform.localPosition = new Vector3(0,Height,0);
        BorderBlocks[6].transform.localPosition = new Vector3(Width,Height,0);
        BorderBlocks[7].transform.localPosition = new Vector3(Width,0,0);
        border.localPosition = new Vector3(-Width/2.0f,-Height/2.0f,0);
    }

    void ResizeBorderTex(GameObject border,float size){
        foreach (var rend in border.GetComponentsInChildren<MeshRenderer>())
        {
            rend.material.mainTextureScale = new Vector2(1,size-2);
        }
    }
}
