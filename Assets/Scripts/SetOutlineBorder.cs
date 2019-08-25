using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineBorder : MonoBehaviour
{
    private LevelStageData Stage;
    void Start()
    {
        Stage = GetComponentInParent<LevelStageData>();
        transform.localScale = new Vector3(Stage.MapWidth,Stage.MapHeight,1);
        // Vector3 pos = new Vector3(Stage.MapWidth/2,Stage.MapHeight/2,0);
        // transform.position = Stage.transform.position+pos;


    }



}
