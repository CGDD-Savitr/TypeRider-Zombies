using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject playerObj;

	PlayerMovement player;

	WordPool wordPool;

	Lane playerLane = Lane.MIDDLE; 

	void Awake()
	{
		player = playerObj.GetComponent<PlayerMovement>();
		wordPool = GetComponent<WordPool>();
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
