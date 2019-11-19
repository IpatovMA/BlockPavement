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
    public GameObject ProgressBar;
    public GameObject lvlShow;

  
    private MapData MapData;

    static public int lvlNum= 0;
    public static lvlState State;
     public Animator DarkScreen;
    ParticleSystem Balloons;

     
    void Awake(){
        LocalizationService.Instance.Load();
        LocalizationService.Instance.SetLang("en");
                // lvlShow.SetActive(true);

    }
    void Start()
    {
        TotalMapsNumber = AllMaps;
        SaveLoad.Load();
        MapData = GetComponentInChildren<MapData>();
        State = lvlState.Menu;
        if( SaveLoad.savedGame.lvl!=0)
           {lvlNum= SaveLoad.savedGame.lvl;}
        else {lvlNum = 1;}

            // lvlShow.SetActive(false);

        ShowLvl();
        Balloons = Camera.main.transform.parent.GetComponentInChildren<ParticleSystem>();

    }


    void FixedUpdate()
    {   
        if (State == lvlState.Menu&&!StartPage.activeSelf){
            // DarkScreen.SetTrigger("Appear");
            StartPage.SetActive(true);

            MapData.LoadMap();


        }
        if(State == lvlState.Play&&ProgressBar.activeSelf){
            SetProgress();
        }

        if(State == lvlState.Fin){
           
           if(!FinPage.activeSelf){
               ProgressBar.SetActive(false);
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
        ProgressBar.SetActive(true);
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
            ShowLvl();
            // lvlShow.SetActive(false);
            FinPage.SetActive(false);
    


    }

    public void Revert(){
        SaveLoad.savedGame = new GameData();
        File.Delete(Application.persistentDataPath + "/savedGame.gd");
        SceneManager.LoadScene("game");
    }
    void ShowLvl(){
        string str = lvlShow.GetComponentInChildren<Text>().text;
        string sep = " ";
        int i = str.IndexOf(sep);
        if (i!=-1) str = str.Substring(0, i);
        lvlShow.GetComponentInChildren<Text>().text = str+" "+lvlNum;
                // Debug.Log(lvlShow.GetComponentInChildren<Text>().text);

    }

    public void SetProgress(){
        if( GetComponentInChildren<PlayerControl>()==null) return;
        int done = GetComponentInChildren<PlayerControl>().DoneBlockCount;
        int total = MapData.TotalBlockCount;
        float fill = ProgressBar.transform.Find("Fill").GetComponent<Image>().fillAmount;
        float newFill =Mathf.Lerp(fill,(float)done/total,Time.fixedDeltaTime*10);
        ProgressBar.transform.Find("Fill").GetComponent<Image>().fillAmount = newFill;
        // Debug.Log((float)done/total+"  " +done+" ");
    }

}

