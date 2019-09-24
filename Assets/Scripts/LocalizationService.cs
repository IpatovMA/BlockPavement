using UnityEngine;
using System.IO;


public class LocalizationService
    {
        private static LocalizationService _instance;
 
        private LocalizationService()
        { }

        public static LocalizationService Instance
        {
            get
            {
                {
                    if (_instance == null)
                        _instance = new LocalizationService();
                    return _instance;
                }
            }
        }

        static LocalizationFileContent LocalizationData;
        static string GameLang;

        public string GetString(string key)
        {
            foreach (var str in LocalizationData.Strings)
            {
                if (str.Key == key){
                    foreach (var val in str.Values)
                    {
                        if(val.Lang == GameLang){
                            return val.Value;
                        }
                    }
                }
            }
         return "error" ;  
        }

        public void Load()
        {
            string json = File.ReadAllText("Assets/Localization/localization.json");
            LocalizationData = JsonUtility.FromJson<LocalizationFileContent>(json);
         
        }

        public void SetLang(string lang)
        {
            GameLang = lang;
        }
    }

