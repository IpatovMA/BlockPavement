using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public enum lvlState {Menu,Play,Fin};
    public GameObject StartPage;
    public GameObject[] Levels;
    public int lvlNum= 0;
     [SerializeField]public static lvlState State;
     public GameObject player;
    // public Camera MenuCam;
    
    void Start()
    {
        State = lvlState.Menu;
        // lvlNum = 0;
            Camera.main.GetComponent<CameraFollow>().target = Levels[lvlNum].GetComponentInChildren<LevelStageData>().gameObject.transform;
            Levels[lvlNum].SetActive(true);
            player = Levels[lvlNum].GetComponentInChildren<PlayerControl>().gameObject;
            player.SetActive(false);

        // MenuCam.GetComponent<CameraFollow>().target = Levels[lvlNum].transform;
    }

    // Update is called once per frame
    void Update()
    {
    Debug.Log(lvlNum);
        if (State == lvlState.Menu&&!Levels[lvlNum].activeSelf){
                        

            Camera.main.GetComponent<CameraFollow>().target = Levels[lvlNum].GetComponentInChildren<LevelStageData>().gameObject.transform;
            Levels[lvlNum].SetActive(true);
            player = Levels[lvlNum].GetComponentInChildren<PlayerControl>().gameObject;
            player.SetActive(false);
            StartPage.SetActive(true);
        }

        if(State == lvlState.Fin&&Input.GetMouseButtonDown(0)){
                if(lvlNum==Levels.Length-1) SceneManager.LoadScene("game");

            Levels[lvlNum].SetActive(false);
            lvlNum++;   
            State = lvlState.Menu; 
            // SceneManager.LoadScene("game");
        }
       
    }

    public void Play(){
        State = lvlState.Play;
        StartPage.SetActive(false);
        player.SetActive(true);

        // Camera.main.gameObject.SetActive(true);
        // MenuCam.gameObject.SetActive(false);
    }
}

