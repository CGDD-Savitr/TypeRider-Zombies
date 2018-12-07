using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour {

	public Text text;
	public float normalDifficulty = 1f;
	public float hardDifficulty = 1.5f;

	// Use this for initialization
	void Start () {
		CrossSceneRegistry.Difficulty = normalDifficulty;
		text.text = "NORMAL";
	}

	public void NextDifficulty()
	{
		if (CrossSceneRegistry.Difficulty == normalDifficulty)
		{
			CrossSceneRegistry.Difficulty = hardDifficulty;
			text.text = "HARD";
		}
		else
		{
			CrossSceneRegistry.Difficulty = normalDifficulty;
			text.text = "NORMAL";
		}
	}
}
