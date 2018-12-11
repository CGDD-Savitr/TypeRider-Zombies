using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	public string ErrorColor = "red";

	public GameObject Controls;

	GameController controller;

	List<Keyword> keywords;

	Animator animator;

	void Awake()
	{
		controller = FindObjectOfType<GameController>();
		animator = Controls.GetComponent<Animator>();
	}

	void Start()
	{
		keywords = new List<Keyword>
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

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			controller.TogglePause();
		}
	}

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		if (!string.IsNullOrEmpty(value))
		{
			bool match = false;
			keywords.ForEach(keyword => 
			{
				if (keyword.Key.StartsWith(value))
				{
					match = true;
					keyword.Text.text = "<color=" + HighlightColor + "><b>" + value + "</b></color>" + keyword.Key.Substring(value.Length);
				}
				else
				{
					keyword.Text.text = keyword.Key;
				}
			});
			if (!match)
			{
				keywords.ForEach(keyword => keyword.Text.text = "<color=" + ErrorColor + "><b>" + keyword.Key + "</b></color>");
			}
		}
	}

	public void OnEndEdit(InputField field)
	{
		Keyword keyword = getMatch(field.text.ToString());
		if (keyword != null)
		{
			controller.MovePlayer(keyword.Value);
			keyword.Key = controller.NextWord(keyword.Key);
		}
		else if (animator)
		{
			animator.Play("uiError", -1, 0f);
		}
		clearHighlights();
		field.text = "";
	}

	Keyword getMatch(string value)
	{
		foreach (Keyword keyword in keywords)
		{
			if (keyword.Key == value)
				return keyword;
		}
		return null;
	}

	void clearHighlights()
	{
		foreach (Keyword keyword in keywords)
			keyword.Text.text = keyword.Key;
	}

	bool highlightSelected(string value)
	{
		bool valid = false; // No keyword matches input value
		bool match = false; // Partial match
		foreach (Keyword keyword in keywords)
		{
			if (keyword.Key.StartsWith(value))
			{
				valid = true;
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
		if (!valid && animator)
			animator.Play("uiError", -1, 0f);
		return match;
	}
}
