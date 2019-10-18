using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class GameData { 
 
    // public static GameData current;
    public int money;
    public int lvl;
    public int map;
 
    public GameData () {
        lvl = 0;
        map = 0;
        money = 0;
    }
         
    public static GameData current(){
        GameData gd = new GameData();
         gd.lvl =SaveLvl();
        gd.map = SaveMap();
        // lvl = LevelManager.lvlNum;
        // map = MapData.mapNum;
        gd.money = 0;
        
        return gd;
    }
    static int SaveLvl(){

         return LevelManager.State == LevelManager.lvlState.Fin? LevelManager.lvlNum+1 : LevelManager.lvlNum;
    }
        static int SaveMap(){

         return LevelManager.State == LevelManager.lvlState.Fin? MapData.mapNum+1 : MapData.mapNum;
    }
}