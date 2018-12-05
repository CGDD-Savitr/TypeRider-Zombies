using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
	public Text LeftKeyword;

	public Text RightKeyword;

	public Text UpKeyword;

	public Text DownKeyword;

	public string HighlightColor = "green";

	GameController controller;

	Keyword[] keywords;

	void Awake()
	{
		controller = FindObjectOfType<GameController>();
	}

	void Start()
	{
		keywords = new Keyword[]
		{
			new Keyword
			{
				Text = UpKeyword,
				Value = Direction.UP,
				Key = UpKeyword.text
			},
			new Keyword
			{
				Text = DownKeyword,
				Value = Direction.DOWN,
				Key = DownKeyword.text
			},
			new Keyword
			{
				Text = RightKeyword,
				Value = Direction.RIGHT,
				Key = RightKeyword.text
			},
			new Keyword
			{
				Text = LeftKeyword,
				Value = Direction.LEFT,
				Key = LeftKeyword.text
			}
		};
	}

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		if (string.IsNullOrEmpty(value))
			return;
		bool match = false;
		foreach (Keyword keyword in keywords)
		{
			if (keyword.Key.StartsWith(value))
			{
				string richText = "<color=" + HighlightColor + "><b>" + value + "</b></color>";
				Debug.Log(richText);
				if (keyword.Key.Length > value.Length)
				{
					match = true;
					richText += keyword.Key.Substring(value.Length);
				}
				else
				{
					controller.MovePlayer(keyword.Value);
					keyword.Key = controller.NextWord(keyword.Key);
					richText = keyword.Key;
				}
				keyword.Text.text = richText;
			}
			else
			{
				keyword.Text.text = keyword.Key;
			}
		}
		if (!match)
			field.text = "";
	}
}
