using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject playerObj;

	PlayerMovement player;

	Lane playerLane = Lane.MIDDLE; 

	void Awake()
	{
		player = playerObj.GetComponent<PlayerMovement>();
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
