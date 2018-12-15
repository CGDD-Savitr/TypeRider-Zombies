using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFocusInput : MonoBehaviour {
	InputField inputField;
	
	void Awake()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("PlayerInput");
		if (obj != null)
		{
			inputField = obj.GetComponent<InputField>();
			if (inputField == null)
			{
				Debug.Log("PlayerInput is not InputField");
			}
		}
		else 
		{
			Debug.Log("No component with PlayerInput tag");
		}
	}
	
	void Update()
	{
		if (!inputField.isFocused)
		{
			inputField.Select();
			inputField.ActivateInputField();
			inputField.text = "";
		}
	}
}
