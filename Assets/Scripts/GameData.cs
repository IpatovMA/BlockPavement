using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class GameData { 
 
    // public static GameData current;
    public int money;
    public int lvl;
    public int map;

    public int rotateOn;
 
    public GameData () {
        lvl = 0;
        map = 0;
        money = 0;
        rotateOn =0;
    }
         
    public static GameData current(){
        GameData gd = new GameData();
         gd.lvl =SaveLvl();
        gd.map = SaveMap();
        gd.rotateOn = MapData.RotateOn;
        // lvl = LevelManager.lvlNum;
        // map = MapData.mapNum;
        gd.money = 0;
        
        return gd;
    }
    static int SaveLvl(){

         return LevelManager.State == LevelManager.lvlState.Fin? LevelManager.lvlNum+1 : LevelManager.lvlNum;
    }
        static int SaveMap(){
            int map;
            if(LevelManager.State == LevelManager.lvlState.Fin){
                if(LevelManager.TotalMapsNumber== MapData.mapNum ){
                        map = 1;
                } else{ map = MapData.mapNum+1;}
            } else{
                map = MapData.mapNum;
            }

         return map;
    }
}