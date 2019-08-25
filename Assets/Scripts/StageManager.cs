using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{
    public GameObject[] LevelStages;
    public GameObject player;
    private int LevelStage = 0;
    private Camera cam;
    private CameraFollow camFollow;
    
    private LevelStageData StageData;

    private bool fin = false;

    void Start()
    {

    }

    void Update()
    {
        if (LevelManager.State != LevelManager.lvlState.Play) return;
        if (cam ==null){
        cam = Camera.main;
        camFollow = cam.GetComponent<CameraFollow>();
        UpdateStage();
        }
        if(StageData.TotalBlockCount==player.GetComponent<BlockPaver>().DoneBlockCount){
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
        camFollow.target = LevelStages[LevelStage].transform;
        StageData = LevelStages[LevelStage].GetComponent<LevelStageData>();
        camFollow.offset = StageData.MapWidth%2==1 ? 0.5f: 0f;
        
        LevelStages[LevelStage].SetActive(true);
        player = LevelStages[LevelStage].GetComponentInChildren<PlayerControl>().gameObject;
        SwipeControl.ResetFp();
        SwipeControl.BlockSwipeInput();
            Invoke("AllowSwipeInput",0.8f);
        
    }
    void FinalView(){
        camFollow.target = LevelStages[0].transform;
        // camFollow.smoothTime =0.8f;
        //  cam.transform.Rotate(0,0,90); 
        // cam.GetComponent<Camera>().orthographicSize = 11;
        fin=true;
    }
    void HideMap(){
        if(LevelStage>0) LevelStages[LevelStage-1].SetActive(false);
    }
    void AllowSwipeInput(){
        SwipeControl.AllowSwipeInput();
    }

}
