using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject Player;
	public GameObject SceneManagerObject;

	public Text PlayerHP;

	public Text MilestoneText;

	public int playerHP = 5;

	public bool Running { get { return !lost; } }

	PlayerMovement player;

	WordPool wordPool;

	bool lost = false;

	bool paused = false;

	Lane playerLane = Lane.BOTTOM_MIDDLE;

	int wordLength = 5;

	Stack<int> milestones;

	int currentTarget;

	void Awake()
	{
		player = Player.GetComponent<PlayerMovement>();
		wordPool = GetComponent<WordPool>();
	}

	void Start()
	{
		Time.timeScale = 1.0f;
		PlayerHP.text = playerHP.ToString();
		wordLength = wordPool.ShortestWordLength;
		milestones = new Stack<int>(CrossSceneRegistry.HighScores);
	}

	void Update()
	{
		int currentScore = CrossSceneRegistry.PlayerScore;
		if (currentTarget != -1 && currentScore > currentTarget)
		{
			currentTarget = milestones.Count > 0 ? milestones.Pop() : -1;
			if (currentTarget != -1)
			{
				MilestoneText.text = "Next high score is " + currentTarget.ToString();
			}
			else
			{
				MilestoneText.text = "You are currently the top scorer!";
			}
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
		// CrossSceneRegistry.PlayerScore = playerScore;
		SceneManagerObject.SendMessage("GameOver");
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
			case Direction.UP:
				MovePlayerUp();
				break;
			case Direction.DOWN:
				MovePlayerDown();
				break;
			default:
				break;
		}
	}

	public void IncrementWordLength()
	{
		if (wordLength < wordPool.LongestWordLength)
			wordLength++;
	}

	public string NextWord()
	{
		return wordPool.GetWord(wordLength);
	}

	public string NextWord(string old)
	{
		wordPool.ReturnWord(old);
		return NextWord();
	}

	public void MovePlayerLeft() 
	{
		if (player.CanMove && (playerLane != Lane.BOTTOM_LEFT) && (playerLane != Lane.TOP_LEFT))
		{
			player.MoveLeft();
			playerLane--;
		}
	}

	public void MovePlayerRight()
	{
		if (player.CanMove && (playerLane != Lane.BOTTOM_RIGHT) && (playerLane != Lane.TOP_RIGHT))
		{
			player.MoveRight();
			playerLane++;
		}
	}

	public void MovePlayerUp()
	{
		if (player.CanMove && (playerLane < Lane.TOP_LEFT))
		{
			player.MoveUp();
			playerLane += 3;
		}
	}

	public void MovePlayerDown()
	{
		if (player.CanMove && (playerLane >= Lane.TOP_LEFT))
		{
			player.MoveDown();
			playerLane -= 3;
		}
	}

	void SaveHighScore()
	{
		List<HighScore> scores = new List<HighScore>();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		HighScores data = new HighScores();

		if (File.Exists(Application.persistentDataPath + "/TypeRiderHighScoresTimestamped.dat"))
		{
			file = File.Open(Application.persistentDataPath + "/TypeRiderHighScoresTimestamped.dat", FileMode.Open);
			data = (HighScores)bf.Deserialize(file);
			file.Close();

			scores = data.scores;
		}

		file = File.Open(Application.persistentDataPath + "/TypeRiderHighScoresTimestamped.dat", FileMode.OpenOrCreate);

		scores.Add(new HighScore
		{
			Score = CrossSceneRegistry.PlayerScore,
			Timestamp = DateTime.Now
		});
		data.scores = scores;

		bf.Serialize(file, data);
		file.Close();
	}

	enum Lane {
		BOTTOM_LEFT = 0,
		BOTTOM_MIDDLE = 1,
		BOTTOM_RIGHT = 2,
		TOP_LEFT = 3,
		TOP_MIDDLE = 4,
		TOP_RIGHT = 5
	};
}
