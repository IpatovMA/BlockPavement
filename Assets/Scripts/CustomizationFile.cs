using System;
[Serializable]public class CustomizationFile{
        public UnitInfo[] Infos;
        [Serializable]public class UnitInfo{
        public string Key;
        public int Num;

        }
    }