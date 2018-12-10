using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	public Text PlayerScoreText;

	public int Base = 10;

	public PlayerMovement Player;

	GameController gameController;

	int playerScore = 0;

	int threshold = 0;

	void Awake()
	{
		gameController = FindObjectOfType<GameController>();
	}

	void Start()
	{
		threshold = CrossSceneRegistry.Difficulty.ScoreThreshold;
		StartCoroutine(ScoreUpdater());
	}

	IEnumerator ScoreUpdater()
	{
		while (gameController.Running)
		{
			AddScore((int) Mathf.Ceil(Player.currentSpeed * CrossSceneRegistry.Difficulty.Multiplier * Base));
			yield return new WaitForSeconds(.1f);
		}
	}

	public void AddScore(int score)
	{
		playerScore += score;
		PlayerScoreText.text = playerScore.ToString();
		if (playerScore > threshold)
		{
			gameController.IncrementWordLength();
			threshold *= 2;
		}
		CrossSceneRegistry.PlayerScore = playerScore;
	}
}
