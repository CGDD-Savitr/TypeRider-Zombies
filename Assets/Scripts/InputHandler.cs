using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
	public Text LeftKeyword;

	public Text RightKeyword;

	GameController controller;

	void Awake()
	{
		controller = FindObjectOfType<GameController>();
	}

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		if (value == RightKeyword.text)
		{
			controller.MovePlayerRight();
			// Change right keyword
			field.text = "";
		}
		else if (value == LeftKeyword.text)
		{
			controller.MovePlayerLeft();
			// Change left keyword
			field.text = "";
		}
	}
}
