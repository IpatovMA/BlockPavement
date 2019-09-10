using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{

    public void UpdateBorder(MapData Data){
        transform.localScale = new Vector3(Data.MapWidth,Data.MapHeight,1);
    }


}
