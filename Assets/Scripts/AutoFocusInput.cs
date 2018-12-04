using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFocusInput 	: MonoBehaviour {
	void Start () {
		GameObject obj = GameObject.FindGameObjectWithTag("PlayerInput");
		if (obj != null)
		{
			InputField inputField = obj.GetComponent<InputField>();
			if (inputField != null)
			{
				Debug.Log("PlayerInput selected");
				inputField.Select();
				inputField.ActivateInputField();
			}
			else
			{
				Debug.Log("PlayerInput is not InputField");
			}
		}
		else 
		{
			Debug.Log("No component with PlayerInput tag");
		}
	}
}
