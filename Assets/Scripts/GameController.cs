using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {
	public GameObject Player;
	public GameObject SceneManagerObject;

	public Text PlayerScore;
	public Text PlayerHP;

	public int playerHP = 5;

	PlayerMovement player;

	WordPool wordPool;

	int playerScore = 0;
	float currentDifficulty = 1f;

	bool lost = false;
	bool paused = false;

	Lane playerLane = Lane.MIDDLE;

	void Awake()
	{
		player = Player.GetComponent<PlayerMovement>();
		wordPool = GetComponent<WordPool>();
	}

	void Start()
	{
		Time.timeScale = 1.0f;
		PlayerHP.text = playerHP.ToString();
		currentDifficulty = DifficultySetting.Difficulty;
	}

	void Update()
	{
		if (!lost)
		{
			AddScore((int)((Mathf.Ceil(Time.deltaTime * 25)) * currentDifficulty * player.currentSpeed));
		}
	}

	public void TogglePause()
	{
		if (!paused)
		{
			Time.timeScale = 0.0f;
			SceneManagerObject.SendMessage("Pause");
		}
		else
		{
			Time.timeScale = 1.0f;
			SceneManagerObject.SendMessage("Unpause");
		}

		paused = !paused;
	}

	public void TakeDamage()
	{
		playerHP--;
		PlayerHP.text = playerHP.ToString();

		if (playerHP <= 0)
		{
			Lose();
		}
	}

	void Lose()
	{
		lost = true;
		SaveHighScore();
		SceneManagerObject.SendMessage("GameOver");
	}

	void SaveHighScore()
	{
		List<int> scores = new List<int>();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		HighScores data = new HighScores();

		if (File.Exists(Application.persistentDataPath + "/TypeRiderHighScores.dat"))
		{
			file = File.Open(Application.persistentDataPath + "/TypeRiderHighScores.dat", FileMode.Open);
			data = (HighScores)bf.Deserialize(file);
			file.Close();

			scores = data.scores;
		}

		file = File.Open(Application.persistentDataPath + "/TypeRiderHighScores.dat", FileMode.OpenOrCreate);

		scores.Add(playerScore);
		data.scores = scores;

		bf.Serialize(file, data);
		file.Close();
	}

	public void AddScore(int score)
	{
		playerScore += score;
		PlayerScore.text = playerScore.ToString();
	}

	public void MovePlayer(Direction direction)
	{
		switch (direction)
		{
			case Direction.LEFT:
				MovePlayerLeft();
				break;
			case Direction.RIGHT:
				MovePlayerRight();
				break;
			default:
				break;
		}
	}

	public string NextWord(string old)
	{
		string word = wordPool.GetWord();
		wordPool.ReturnWord(old);
		return word;
	}

	public void MovePlayerLeft() 
	{
		if (player.CanMove && playerLane > Lane.LEFT)
		{
			player.MoveLeft();
			playerLane--;
		}
	}

	public void MovePlayerRight()
	{
		if (player.CanMove && playerLane < Lane.RIGHT)
		{
			player.MoveRight();
			playerLane++;
		}
	}

	public void MovePlayerUp()
	{
		Debug.Log("Move up");
	}

	public void MovePlayerDown()
	{
		Debug.Log("Move down");
	}

	enum Lane {
		LEFT = 0,
		MIDDLE = 1,
		RIGHT = 2
	};
}
