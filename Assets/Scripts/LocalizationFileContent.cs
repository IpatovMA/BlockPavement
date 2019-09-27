using System;
using UnityEngine;

 [Serializable] public class LocalizationFileContent {
		public LocalizationString[] Strings;
		public LocalizationFormat[] Format;

	 [Serializable]public class LocalizationString {
		 	public string Key;
			public SingleLang[] Values;
}
	 [Serializable]public class LocalizationFormat {
		 	public string Lang;
		 	// public int Size;
			public string Font;
			public int Style;
}
 [Serializable]public class SingleLang {
public string Lang;
public string Value;
}
	}
