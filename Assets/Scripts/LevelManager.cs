using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public GameObject[] LevelStages;
    public GameObject player;
    private int LevelStage = 0;
    private Camera cam;
    private bool fin = false;

    void Start()
    {
        cam = Camera.main;
        UpdateStage();

    }

    void Update()
    {
        if(LevelStages[LevelStage].GetComponent<LevelStageData>().TotalBlockCount==player.GetComponent<BlockPaver>().DoneBlockCount){
            player.SetActive(false);
            if(LevelStage<LevelStages.Length-1){
            LevelStage++;
            UpdateStage();
            // Invoke("UpdateStage",0.2f);
            }else if(!fin) {
                FinalView();
            // Invoke("FinalView",0.2f);
            }
        } 

        if(fin&&Input.GetMouseButtonDown(0)){
                SceneManager.LoadScene("game");
        }
    }
    void UpdateStage(){
        cam.GetComponent<CameraFollow>().target = LevelStages[LevelStage].transform;
        LevelStages[LevelStage].SetActive(true);
        player = LevelStages[LevelStage].GetComponentInChildren<PlayerControl>().gameObject;
        SwipeControl.ResetFp();
        SwipeControl.BlockSwipeInput();
            Invoke("AllowSwipeInput",0.8f);
        
    }
    void FinalView(){
        cam.GetComponent<CameraFollow>().target = LevelStages[1].transform;
        cam.transform.Rotate(0,0,90); 
        cam.GetComponent<Camera>().orthographicSize = 14;
        fin=true;
    }
    void HideMap(){
        if(LevelStage>0) LevelStages[LevelStage-1].SetActive(false);
    }
    void AllowSwipeInput(){
        SwipeControl.AllowSwipeInput();
    }

}
