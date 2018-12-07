using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour {

	public Text text;
	public float normalDifficulty = 1f;
	public float hardDifficulty = 1.5f;

	// Use this for initialization
	void Start () {
		DifficultySetting.Difficulty = normalDifficulty;
		text.text = "NORMAL";
	}

	public void NextDifficulty()
	{
		if (DifficultySetting.Difficulty == normalDifficulty)
		{
			DifficultySetting.Difficulty = hardDifficulty;
			text.text = "HARD";
		}
		else
		{
			DifficultySetting.Difficulty = normalDifficulty;
			text.text = "NORMAL";
		}
	}
}
