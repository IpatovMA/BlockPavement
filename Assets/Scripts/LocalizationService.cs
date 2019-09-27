using UnityEngine;
using UnityEngine.UI;

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
        static LocalizationFileContent.LocalizationFormat CurrentFormat;


        public string GetString(string key)
        {
            
            foreach (var str in LocalizationData.Strings)
            {
                if (str.Key == key){
                    foreach (var val in str.Values)
                    {
                        if(val.Lang == GameLang){
                            return val.Value.ToUpper();
                        }
                    }
                }
            }
         return "error" ;  
        }
         void GetFormat(){
            // Text TextFormat = new Text();
             foreach (var format in LocalizationData.Format)
            {
                if(format.Lang == GameLang){
                   CurrentFormat = format;
                    return;
                }             
            }
            CurrentFormat = LocalizationData.Format[0];
        }

        public void SetFormat(Text txt){
            txt.fontStyle = (FontStyle)CurrentFormat.Style;
            Font font=(Font)Resources.Load("Fonts/"+CurrentFormat.Font); 
            txt.font = font;
            // Debug.Log(JsonUtility.ToJson(txt));

        }

        public void Load()
        {   
            
            TextAsset JsonData=(TextAsset)Resources.Load("Localization/localization"); 
            string json=JsonData.text;
            LocalizationData = JsonUtility.FromJson<LocalizationFileContent>(json);
            // Debug.Log(JsonUtility.ToJson(LocalizationData));
            
        }

        public void SetLang(string lang)
        {
            GameLang = lang;
            GetFormat();
        }
    }

