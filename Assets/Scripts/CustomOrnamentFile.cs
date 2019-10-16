using System;

    [Serializable]public class CustomOrnamentFile{

        [Serializable]public class SingleUzor{
            public string UzorColor;
            public string BackColor;   
        }

        public SingleUzor[] Ornaments;
    }