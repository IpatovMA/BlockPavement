using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum lvlState {Menu,Play,Fin};
    public GameObject[] Levels;
    public int lvlNum;
    public static lvlState State;
    public Camera MenuCam;
    
    void Start()
    {
        State = lvlState.Menu;
        lvlNum = 0;
        MenuCam.GetComponent<CameraFollow>().target = Levels[lvlNum].GetComponentInChildren<LevelStageData>().gameObject.transform;
        // MenuCam.GetComponent<CameraFollow>().target = Levels[lvlNum].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void IncLvlState(){
    //     State = Convert.ToInt32(State) +1;
    // }
}
