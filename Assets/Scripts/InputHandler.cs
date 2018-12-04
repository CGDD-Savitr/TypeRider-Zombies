using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
	GameController controller;

	void Awake()
	{
		controller = FindObjectOfType<GameController>();
	}

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		Debug.Log("Input: " + value);
		if (value == "right")
		{
			controller.MovePlayerRight();
			field.text = "";
		}
	}
}
