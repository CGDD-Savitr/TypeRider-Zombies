using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject Player;

	public Text PlayerScore;

	PlayerMovement player;

	WordPool wordPool;

	int playerScore = 0;

	Lane playerLane = Lane.MIDDLE; 

	void Awake()
	{
		player = Player.GetComponent<PlayerMovement>();
		wordPool = GetComponent<WordPool>();
	}

	void Update()
	{
		AddScore((int)(Mathf.Ceil(Time.deltaTime * 25)));
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
