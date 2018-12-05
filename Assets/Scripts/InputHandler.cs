﻿using System.Collections;
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
		{
			clearHighlights();
			return;
		}
		if (!highlightSelected(value))
			field.text = ""; // Clear input if nothing is highlighted
	}

	void clearHighlights()
	{
		foreach (Keyword keyword in keywords)
		{
			keyword.Text.text = keyword.Key;
		}
	}

	bool highlightSelected(string value)
	{
		bool match = false; // No keyword matches input value
		foreach (Keyword keyword in keywords)
		{
			if (keyword.Key.StartsWith(value))
			{
				string richText = "<color=" + HighlightColor + "><b>" + value + "</b></color>"; // Add style to matched section
				if (keyword.Key.Length > value.Length)
				{
					// Partial match
					match = true;
					richText += keyword.Key.Substring(value.Length);
				}
				else
				{
					// Full match
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
		return match;
	}
}
