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


        public string GetString(string key)
        {
            string i = "dfdf";
         return i ;  
        }

        public string Load()
        {
                        string i = "dfdf";
         return i ;  
        }

        public void SetLang(string lang)
        {
        }
    }

