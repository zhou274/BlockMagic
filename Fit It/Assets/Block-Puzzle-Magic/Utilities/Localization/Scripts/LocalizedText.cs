using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
	public string txtTag;
	//TextMeshProUGUI thisText;

	void Awake ()
	{
		//thisText = GetComponent<TextMeshProUGUI> ();
	}

	void OnEnable ()
	{
		//LocalizationManager.OnLanguageChangedEvent += OnLanguageChangedEvent;
		//Invoke ("SetLocalizedText", 0.01F);
	}

	void OnDisable ()
	{
		//LocalizationManager.OnLanguageChangedEvent -= OnLanguageChangedEvent;
	}

	public void SetLocalizedText ()
	{
		//if (!string.IsNullOrEmpty (txtTag.Trim ())) {
		//	TextMeshProUGUI.SetLocalizedTextForTag (txtTag);
		//}
	}

	void OnLanguageChangedEvent (string langCode)
	{
		//SetLocalizedText ();
	}
}