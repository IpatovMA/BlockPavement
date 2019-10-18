using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
 
public static class SaveLoad {
 
    public static GameData savedGame =  new GameData();
             
    //методы загрузки и сохранения статические, поэтому их можно вызвать откуда угодно
    public static void Save() {
    //    Debug.Log("save");
        SaveLoad.savedGame =  GameData.current();
        // Debug.Log( SaveLoad.savedGame.map);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath это строка; выведите ее в логах и вы увидите расположение файла сохранений
        FileStream file = File.Create (Application.persistentDataPath + "/savedGame.gd");
        bf.Serialize(file, SaveLoad.savedGame);
        file.Close();
    }   
     
    public static void Load() {
    //    Debug.Log("load");
        if(File.Exists(Application.persistentDataPath + "/savedGame.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            SaveLoad.savedGame = (GameData)bf.Deserialize(file);
            file.Close();
        }
    }
}