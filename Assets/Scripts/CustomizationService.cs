using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationService : MonoBehaviour
{
    
    
    public struct CurrentModel
    {
        public string Key;
        public GameObject Model;
        public int Num;
    }
   public CustomUnit[] Units;
   public List<CurrentModel> CurrentModels= new List<CurrentModel>();
//    public CurrentModel[] CurrentModels;


    public CustomizationFile UnitsData;
    void Awake(){
        // UnitsData = new CustomizationFile();
        // CustomizationFile.UnitInfo mod1 = new CustomizationFile.UnitInfo();
        // CustomizationFile.UnitInfo mod2 = new CustomizationFile.UnitInfo();
        // mod1.Key = "Car";
        // mod1.Num = 1;
        // mod2.Key = "dust";
        // mod2.Num = 0;

        // UnitsData.Infos = new CustomizationFile.UnitInfo[2]{mod1,mod2};
        // Debug.Log(JsonUtility.ToJson(UnitsData));
        ReloadFile();
        LoadAllModels();

    }

    void ReloadFile(){

            TextAsset JsonData=(TextAsset)Resources.Load("Configs/customization"); 
            string json=JsonData.text;
            // Debug.Log(json);
            UnitsData = JsonUtility.FromJson<CustomizationFile>(json);
        // Debug.Log(JsonUtility.ToJson(UnitsData.Infos[0].Key));

    }

    public void LoadAllModels(){
        
        foreach (var unit in Units){

            foreach (var info in UnitsData.Infos)
            {
                if(info.Key == unit.Key){
                    CurrentModel mod = new CurrentModel();
                    mod.Model =unit.Models[info.Num];
                    mod.Key = unit.Key;
                    mod.Num = info.Num;
                    CurrentModels.Add(mod);  
                }
            }
        
        }
    }
    public GameObject SetModel(string Key){
        // Debug.Log("fdf");
        foreach (var mod in CurrentModels)
        {
            if (mod.Key == Key){
                
                return mod.Model;
            }
        }
        return null;
    }
}
