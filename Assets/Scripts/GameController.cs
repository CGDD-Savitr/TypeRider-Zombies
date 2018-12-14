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

	public GameObject PlayerInput;

	public GameObject PlayerHPSliderObject;
	Slider PlayerHPSlider;

	public Text MilestoneText;

	public int playerHP = 5;

	public bool Running { get { return !lost; } }

	PlayerMovement player;

	WordPool wordPool;

	bool lost = false;

	bool paused = false;

	Lane playerLane = Lane.BOTTOM_MIDDLE;

	int wordLength = 5;

	bool typingControls = true;

	Stack<int> milestones;

	float damageCooldown = 1f;

	bool canTakeDamage = true;

	int currentTarget;

	void Awake()
	{
		player = Player.GetComponent<PlayerMovement>();
		wordPool = GetComponent<WordPool>();
		Cursor.visible = false;
		wordLength = wordPool.ShortestWordLength;
	}

	void Start()
	{
		Time.timeScale = 1.0f;

		PlayerHPSlider = PlayerHPSliderObject.GetComponent<Slider>();
		PlayerHPSlider.maxValue = playerHP;
		PlayerHPSlider.value = playerHP;

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
				string suffix = "th";
				if (milestones.Count == 2)
					suffix = "rd";
				else if (milestones.Count == 1)
					suffix = "nd";
				else if (milestones.Count == 0)
					suffix = "st";
				MilestoneText.text = milestones.Count + 1 + suffix + " place high score is " + currentTarget.ToString();
			}
			else
			{
				MilestoneText.text = "You are currently the top scorer!";
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale *= 2f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale *= 0.5f;
        }
	}

	public void TogglePause()
	{
		if (!paused)
		{
			Time.timeScale = 0.0f;
			SceneManagerObject.SendMessage("Pause");
			Cursor.visible = true;
		}
		else
		{
			Time.timeScale = 1.0f;
			SceneManagerObject.SendMessage("Unpause");
			Cursor.visible = false;
		}

		paused = !paused;
	}

	public bool TakeDamage()
	{
		if (canTakeDamage)
		{
			playerHP--;
			PlayerHPSlider.value = playerHP;

			if (playerHP <= 0)
			{
				Lose();
			}
			else
			{
				StartCoroutine(PlayerInvulnerable());
			}
			return true;
		}
		return false;
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

	public void EnablePowerUp(int powerup)
	{
		if (powerup >= 0 && powerup <= 2 && CrossSceneRegistry.CanUsePower[powerup])
		{
			CrossSceneRegistry.ActivatedPower[powerup] = true;
			CrossSceneRegistry.CanUsePower[powerup] = false;
		}
	}

	public void ToggleControls()
	{
		if (typingControls)
		{
			GetComponent<KeyboardControls>().enabled = true;
			PlayerInput.GetComponent<InputField>().enabled = false;
			GetComponent<InputHandler>().DisplayWASD();
		}
		else
		{
			GetComponent<KeyboardControls>().enabled = false;
			PlayerInput.GetComponent<InputField>().enabled = true;
			GetComponent<InputHandler>().DisplayKeyword();
		}
		typingControls = !typingControls;
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

	IEnumerator PlayerInvulnerable()
	{
		canTakeDamage = false;
		Material mat = player.GetComponentInChildren<MeshRenderer>().material;
		TrailRenderer trail = player.GetComponentInChildren<TrailRenderer>();
		Color baseC = mat.GetColor("_EmissionColor");
		Color trailC = trail.startColor;
		mat.SetColor("_EmissionColor", Color.red);
		trail.startColor = Color.red;
		int iter = 10;
		float t = damageCooldown / iter;
		for (int i = 1; i < iter + 1; ++i)
		{
			mat.SetColor("_EmissionColor", Color.Lerp(Color.red, baseC, (t * i) / damageCooldown));
			trail.startColor = Color.Lerp(Color.red, trailC, (t * i) / damageCooldown);
			yield return new WaitForSeconds(t);
		}
		canTakeDamage = true;
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
