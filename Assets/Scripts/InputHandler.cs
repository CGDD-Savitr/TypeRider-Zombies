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

	public GameObject Controls;

	GameController controller;

	Keyword[] keywords;

	Animator animator;

	void Awake()
	{
		controller = FindObjectOfType<GameController>();
		animator = Controls.GetComponent<Animator>();
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

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			controller.TogglePause();
		}
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale *= 2f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale *= 0.5f;
        }
    }

	public void OnValueChanged(InputField field)
	{
		string value = field.text.ToString();
		if (string.IsNullOrEmpty(value))
		{
			clearHighlights();
			return;
		}
        // if i selected power
        char lastKeyEntered = value[value.Length - 1];
        if (char.IsDigit(lastKeyEntered) || lastKeyEntered == ' ')
        {
            switch (lastKeyEntered)
            {
                case '1':
                    if (CrossSceneRegistry.CanUsePower[0])
                    {
                        CrossSceneRegistry.ActivatedPower[0] = true;
                        CrossSceneRegistry.CanUsePower[0] = false;
                    }
                    break;
                case '2':
                    if (CrossSceneRegistry.CanUsePower[1])
                    {
                        CrossSceneRegistry.ActivatedPower[1] = true;
                        CrossSceneRegistry.CanUsePower[1] = false;
                    }
                    break;
                case '3':
                    if (CrossSceneRegistry.CanUsePower[2])
                    {
                        foreach (Keyword keyword in keywords)
                        {
                            switch (keyword.Value)
                            {
                                case Direction.UP:
                                    keyword.Key = "w";
                                    break;
                                case Direction.LEFT:
                                    keyword.Key = "a";
                                    break;
                                case Direction.DOWN:
                                    keyword.Key = "s";
                                    break;
                                case Direction.RIGHT:
                                    keyword.Key = "d";
                                    break;
                                default:
                                    break;
                            }
                        }

                        CrossSceneRegistry.ActivatedPower[2] = true;
                        CrossSceneRegistry.CanUsePower[2] = false;
                    }
                    break;
            }
                
            field.text = value.Substring(0, value.Length - 1);
        }
		else if (!highlightSelected(value))
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
                    if (!CrossSceneRegistry.ActivatedPower[2])
                    {
                        keyword.Key = controller.NextWord(keyword.Key);
                    }
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
