using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{
    public int uzorNum = 6;
    public int palettesNum=6;
    int currentUzor;
    int currentPalette;
    CustomOrnamentFile OrnamentData;

    void Start(){

        TextAsset JsonData=(TextAsset)Resources.Load("Configs/ornaments"); 
            string json=JsonData.text;
            OrnamentData = JsonUtility.FromJson<CustomOrnamentFile>(json);
            // Debug.Log(JsonUtility.ToJson(LocalizationData));

    }

    public void UpdateBorder(MapData Data){
        transform.Find("Quads").localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
        // transform.Find("collider").localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);

        currentUzor = Random.Range(1,uzorNum+1);
        currentPalette = Random.Range(0,palettesNum);

        SpriteRenderer uzor = GetComponentInChildren<SpriteRenderer>();

            string path = "OrnamentIMG/Ornament"+currentUzor;
            var spr = Resources.Load<Sprite>(path);
            uzor.sprite = spr;

            Color tempColor = new Color();
            ColorUtility.TryParseHtmlString(OrnamentData.Ornaments[currentPalette].UzorColor, out tempColor);
            uzor.color = tempColor;
            uzor.size = new Vector2(Data.MapWidth,Data.MapHeight);
            uzor.transform.localPosition = new Vector3(Data.MapWidth/2.0f,Data.MapHeight/2.0f,uzor.transform.localPosition.z);
        
        ColorUtility.TryParseHtmlString(OrnamentData.Ornaments[currentPalette].BackColor, out tempColor);
        // Debug.Log(( GetComponentInChildren<MeshRenderer>().gameObject.name));
        GetComponentInChildren<MeshRenderer>().material.color = tempColor;

        transform.localPosition = new Vector3(-Data.MapWidth/2.0f,-Data.MapHeight/2.0f,0);

    }


}
