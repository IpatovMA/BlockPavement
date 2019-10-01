using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelManager : MonoBehaviour
{
    public int TotalMapsNumber;
   public enum lvlState {Menu,Play,Fin};
    public GameObject StartPage;
    public GameObject FinPage;
    public GameObject lvlShow;

    // public GameObject[] Levels;
    private MapData MapData;

    static public int lvlNum= 0;
    public static lvlState State;
     public Animator DarkScreen;


     
    void Awake(){
        LocalizationService.Instance.Load();
        LocalizationService.Instance.SetLang("en");
                lvlShow.SetActive(true);

    }
    void Start()
    {

        // LocalizationService.Instance.Load();
        // LocalizationService.Instance.SetLang("ru");

        MapData = GetComponentInChildren<MapData>();
        State = lvlState.Menu;
        lvlNum= 1;
            lvlShow.SetActive(false);

        

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
                ShowLvl();
        lvlShow.SetActive(true);
    }
        void AllowSwipeInput(){
        SwipeControl.AllowSwipeInput();
    }
    public void GetRewards(){
            DarkScreen.SetTrigger("Disappear");
            // ToNextLevel();
            

    }
    


    void ToNextLevel(){
            MapData.DestroyMap();
            State = lvlState.Menu; 
            lvlNum++;   
            ShowLvl();
            lvlShow.SetActive(false);
            FinPage.SetActive(false);


    }

    public void Revert(){
        SceneManager.LoadScene("game");
    }
    void ShowLvl(){
        string str = lvlShow.GetComponentInChildren<Text>().text;
        string sep = " #";
        int i = str.IndexOf(sep);
        if (i!=-1) str = str.Substring(0, i);
        lvlShow.GetComponentInChildren<Text>().text = str+" #"+lvlNum;
                // Debug.Log(lvlShow.GetComponentInChildren<Text>().text);

    }
}

