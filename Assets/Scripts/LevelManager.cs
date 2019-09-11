using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public int TotalLevelNumber;
   public enum lvlState {Menu,Play,Fin};
    public GameObject StartPage;
    public GameObject FinPage;

    // public GameObject[] Levels;
    private MapData MapData;

    static public int lvlNum= 0;
    public static lvlState State;
     public Animator DarkScreen;
     
    
    void Start()
    {
        MapData = GetComponentInChildren<MapData>();
        State = lvlState.Menu;
        lvlNum= 1;
        

    }


    void FixedUpdate()
    {   
        if (State == lvlState.Menu&&!StartPage.activeSelf){
            DarkScreen.SetTrigger("Appear");
            StartPage.SetActive(true);

            MapData.LoadMap();


        }

        if(State == lvlState.Fin){
           
           if(!FinPage.activeSelf){
                FinPage.SetActive(true);
           }
            if (DarkScreen.GetComponent<DarkScreenControl>().Dark){
                ToNextLevel();
            }
        }
       
    }

    public void Play(){
        State = lvlState.Play;
        SwipeControl.ResetFp();
        SwipeControl.BlockSwipeInput();
        Invoke("AllowSwipeInput",0.6f);
        StartPage.SetActive(false);

    }
        void AllowSwipeInput(){
        SwipeControl.AllowSwipeInput();
    }
    public void GetRewards(){
            DarkScreen.SetTrigger("Disappear");
            // ToNextLevel();

    }



    void ToNextLevel(){
            if(lvlNum==TotalLevelNumber) {
                lvlNum=0;
                MapData.RotateOn++;
                
                if(MapData.RotateOn==4){
                    SceneManager.LoadScene("game");

                }
            }
            MapData.DestroyMap();
            State = lvlState.Menu; 
            lvlNum++;   
            FinPage.SetActive(false);


    }
}

