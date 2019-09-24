using System;
 [Serializable] public class LocalizationFileContent {
		public LocalizationString[] Strings;

	 [Serializable]public class LocalizationString {
		 	public string Key;
			public SingleLang[] Values;
}
 [Serializable]public class SingleLang {
public string Lang;
public string Value;
}
	}
