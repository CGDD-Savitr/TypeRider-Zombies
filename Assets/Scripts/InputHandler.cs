using System;
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
		keywords.ForEach(keyword => 
		{
			Text queue = getQueuedWord(keyword);
			queue.text = controller.NextWord();
		});

	}

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		if (!string.IsNullOrEmpty(value))
		{
			if (char.IsDigit(value.Last()))
			{
				int dig = Int32.Parse(value.Last().ToString()) - 1;
				controller.EnablePowerUp(dig);
				field.text = field.text.Substring(0, value.Length - 1);
				return;
			}
			if (value.Last() == ' ')
			{
				field.text = field.text.Substring(0, value.Length - 1);
				return;
			}
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
		else
		{
			keywords.ForEach(keyword => keyword.Text.text = keyword.Key);
		}
	}

	public void OnEndEdit(InputField field)
	{
		Keyword keyword = getMatch(field.text.ToString());
		if (keyword != null)
		{
			controller.MovePlayer(keyword.Value);
			Text queuedWord = getQueuedWord(keyword);
			keyword.Text.text = queuedWord.text;
			string old = keyword.Key;
			queuedWord.text = controller.NextWord(old);
			keyword.Key = keyword.Text.text;
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

	Text getQueuedWord(Keyword keyword)
	{
		foreach (Text text in keyword.Text.GetComponentsInChildren<Text>())
		{
			if (text.tag == "QueueWord")
				return text;
		}
		return null;
	}
}
