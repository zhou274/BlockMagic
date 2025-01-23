using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]

public class UIFontColor : MonoBehaviour 
{
	//TextMeshProUGUI currentText;
	[SerializeField] private string UIColorTag;

	void Awake()
	{
		//currentText = GetComponent<TextMeshProUGUI> ();
	}

	void OnEnable()
	{
		//UIThemeManager.OnUIThemeChangedEvent += OnUIThemeChangedEvent;	
		//Invoke ("UpdateFontUI", 0.1F);
	}

	void OnDisable()
	{
		//UIThemeManager.OnUIThemeChangedEvent -= OnUIThemeChangedEvent;	
	}

	void OnUIThemeChangedEvent (bool isDarkThemeEnabled)
	{
		//UpdateFontUI ();
	}

	void UpdateFontUI() 
	{
		//if (currentText != null) {
		//	UIThemeTag tag = UIThemeManager.Instance.currentUITheme.UIStyle.Find (o => o.tagName == UIColorTag);
		//	if (tag != null) {
		//		currentText.color = tag.UIColor;
		//	}
		//}
	}
}
