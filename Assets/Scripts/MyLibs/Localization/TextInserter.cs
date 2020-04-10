using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/**Class that uses Localization to fill text
 * It automatically updates the text whenever the language is changed in
 * the localization class
*/
public class TextInserter : MonoBehaviour {

	private string extension = null;
	/** Text that are going to be linked to be localized*/
	public UIText[] localizableTexts;

	public void OnEnable(){
		Localization l =	Localization.getInstance ();
		if (extension==null||l.langExtension != extension) {
			Refresh ();
			Localization.onLanguagechanged += Refresh;
		}
	}

	public void OnDisable(){
		Localization.onLanguagechanged-=Refresh;
	}

	

	private void  Refresh(){
		Localization l = Localization.getInstance ();
		extension = l.langExtension;
		foreach (UIText t in localizableTexts) {
			t.text.text = l.get (t.localizatorKey);
		}
	}
	[Serializable]
	public class UIText{
		public UnityEngine.UI.Text text;
		public string localizatorKey;
	}
}
