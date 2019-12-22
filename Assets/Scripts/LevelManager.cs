using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;



public class LevelManager : MonoBehaviour
{
    public static int TotalMapsNumber;
    [SerializeField] int AllMaps;
   public enum lvlState {Menu,Play,Fin};
    public GameObject StartPage;
    public GameObject FinPage;

    private MapData MapData;

    static public int lvlNum= 0;
    public static lvlState State;
     public Animator DarkScreen;
    ParticleSystem Balloons;

    
    public static float TotalBrightness;
    [Space][SerializeField]float ModelsBright;

     
    void Awake(){
        LocalizationService.Instance.Load();
        LocalizationService.Instance.SetLang("en");
                // lvlShow.SetActive(true);

    }
    void Start()
    {
        TotalBrightness = ModelsBright;
        TotalMapsNumber = AllMaps;
        SaveLoad.Load();
        MapData = GetComponentInChildren<MapData>();
        State = lvlState.Menu;
        if( SaveLoad.savedGame.lvl!=0)
           {lvlNum= SaveLoad.savedGame.lvl;}
        else {lvlNum = 1;}

            // lvlShow.SetActive(false);

        Balloons = Camera.main.transform.parent.GetComponentInChildren<ParticleSystem>();

    }


    void FixedUpdate()
    {   
        if(TotalBrightness != ModelsBright){
            TotalBrightness = ModelsBright;
        }


        if (State == lvlState.Menu&&!StartPage.activeSelf){
            // DarkScreen.SetTrigger("Appear");
            StartPage.SetActive(true);

            MapData.LoadMap();


        }


        if(State == lvlState.Fin){
           
           if(!FinPage.activeSelf){
                FinPage.SetActive(true);
                SaveLoad.Save();
                Invoke("OnLvlCompleted",0.5f);
           }
            if (DarkScreen.GetComponent<DarkScreenControl>().Dark){
                ToNextLevel();
            }
        }
       
    }

    void OnLvlCompleted(){
        Vibration.Vibrate(1000);
        
        Balloons.Play();
    }

    public void Play(){
        State = lvlState.Play;
        SwipeControl.ResetFp();
        SwipeControl.BlockSwipeInput();
        StartPage.SetActive(false);
    }
    //     void AllowSwipeInput(){
    //     SwipeControl.AllowSwipeInput();
    // }
    public void GetRewards(){
            DarkScreen.SetTrigger("Disappear");
            // ToNextLevel();
            

    }
    


    void ToNextLevel(){
            Balloons.Stop();
            Balloons.Clear();
            MapData.DestroyMap();
            State = lvlState.Menu; 
            lvlNum++;   
            FinPage.SetActive(false);
    


    }

    public void Revert(){
        SaveLoad.savedGame = new GameData();
        File.Delete(Application.persistentDataPath + "/savedGame.gd");
        SceneManager.LoadScene("game");
    }




}

