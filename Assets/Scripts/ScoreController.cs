using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	public Text PlayerScoreText;

	public int Threshold = 10000;

	public int Base = 10;

	public PlayerMovement Player;

	GameController gameController;

	int playerScore = 0;

	void Awake()
	{
		gameController = FindObjectOfType<GameController>();
	}

	void Start()
	{
		StartCoroutine(ScoreUpdater());
	}

	IEnumerator ScoreUpdater()
	{
		while (gameController.Running)
		{
			AddScore((int) Mathf.Ceil(Player.currentSpeed * CrossSceneRegistry.Difficulty * Base));
			yield return new WaitForSeconds(.1f);
		}
	}

	public void AddScore(int score)
	{
		playerScore += score;
		PlayerScoreText.text = playerScore.ToString();
		if (playerScore > Threshold)
		{
			gameController.IncrementWordLength();
			Threshold *= 2;
		}
		CrossSceneRegistry.PlayerScore = playerScore;
	}
}
