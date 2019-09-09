using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public enum lvlState {Menu,Play,Fin};
    public GameObject StartPage;
    public GameObject FinPage;

    public GameObject[] Levels;
    public int lvlNum= 0;
    public static lvlState State;
     public GameObject player;
     public Animator DarkScreen;
     
    // public Camera MenuCam;
    
    void Start()
    {
        State = lvlState.Menu;


        // lvlNum = 0;
            Camera.main.GetComponent<CameraFollow>().target = Levels[lvlNum].GetComponentInChildren<MapData>().gameObject.transform;
            Levels[lvlNum].SetActive(true);
            player = Levels[lvlNum].GetComponentInChildren<PlayerControl>().gameObject;
            player.SetActive(false);
            StartPage.SetActive(true);
        // MenuCam.GetComponent<CameraFollow>().target = Levels[lvlNum].transform;
    }


    // Update is called once per frame
    void FixedUpdate()
    {   
        // Debug.Log(State+"  "+StartPage.activeSelf);
        if (State == lvlState.Menu&&!StartPage.activeSelf){
            DarkScreen.SetTrigger("Appear");
                        StartPage.SetActive(true);

            Camera.main.GetComponent<CameraFollow>().target = Levels[lvlNum].GetComponentInChildren<MapData>().gameObject.transform;
        //    if (Camera.main.GetComponent<CameraFollow>().target.position.x == Camera.main.transform.position.x){
                // if (!player.activeSelf){
            Levels[lvlNum].SetActive(true);
                // }            
            player = Levels[lvlNum].GetComponentInChildren<PlayerControl>().gameObject;
            player.SetActive(false);
            
            // Debug.Log("fdsfdsf");
            //}
        }

        if(State == lvlState.Fin){
           
           if(!FinPage.activeSelf){
                FinPage.SetActive(true);
           }
            // Debug.Log(DarkScreen.GetComponent<DarkScreenControl>().Dark);
            if (DarkScreen.GetComponent<DarkScreenControl>().Dark){
                ToNextLevel();
            }
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
    public void GetRewards(){
            DarkScreen.SetTrigger("Disappear");
            // ToNextLevel();

    }



    void ToNextLevel(){
            if(lvlNum==Levels.Length-1) SceneManager.LoadScene("game");
            State = lvlState.Menu; 
            Levels[lvlNum].SetActive(false);
            lvlNum++;   
            FinPage.SetActive(false);


    }
}

