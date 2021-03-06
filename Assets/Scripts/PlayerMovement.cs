﻿using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float initialForwardVelocity = 0.5f;
	public float laneChangeVelocity = 2f;

	[HideInInspector]
	public float currentSpeed;

	private float timeToChangeLane;
	private float currentDifficulty;

	private bool moving = false;

	private Rigidbody rigid;

	private Vector3 changeLaneVectorLeft;
	private Vector3 changeLaneVectorRight;
	private Vector3 changeLaneVectorUp;
	private Vector3 changeLaneVectorDown;
	private Vector3 forwardVector;
    private Vector3 playerPos;

	void Start ()
	{
		rigid = GetComponent<Rigidbody>();
		currentDifficulty = CrossSceneRegistry.Difficulty.InitialVelocity;
		timeToChangeLane = 1 / laneChangeVelocity;
		forwardVector = new Vector3(0f, 0f, initialForwardVelocity * currentDifficulty);
		changeLaneVectorLeft = new Vector3(-laneChangeVelocity, 0f, initialForwardVelocity * currentDifficulty);
		changeLaneVectorRight = new Vector3(laneChangeVelocity, 0f, initialForwardVelocity * currentDifficulty);
		changeLaneVectorUp = new Vector3(0f, laneChangeVelocity, initialForwardVelocity * currentDifficulty);
		changeLaneVectorDown = new Vector3(0f, -laneChangeVelocity, initialForwardVelocity * currentDifficulty);
		rigid.velocity = forwardVector;
        playerPos = new Vector3(0f, 0f, 0f);
		currentSpeed = forwardVector.z;
	}

	void Update()
	{
		forwardVector.z += currentDifficulty * Time.deltaTime * 0.01f;
		changeLaneVectorLeft.z += currentDifficulty * Time.deltaTime * 0.01f;
		changeLaneVectorRight.z += currentDifficulty * Time.deltaTime * 0.01f;
		changeLaneVectorUp.z += currentDifficulty * Time.deltaTime * 0.01f;
		changeLaneVectorDown.z += currentDifficulty * Time.deltaTime * 0.01f;
		currentSpeed = forwardVector.z;
	}

	void FixedUpdate()
	{
		if (!moving)
		{
			rigid.velocity = forwardVector;
		}
	}

	public void MoveLeft()
	{
		Move(changeLaneVectorLeft);
	}

	public void MoveRight()
	{
		Move(changeLaneVectorRight);
	}

	public void MoveUp()
	{
		Move(changeLaneVectorUp);
	}

	public void MoveDown()
	{
		Move(changeLaneVectorDown);
	}

	public bool CanMove { get { return !moving; } }

	void Move(Vector3 changeLaneVector)
	{
		if (!moving)
		{
			rigid.velocity = changeLaneVector;
			moving = true;
			Invoke("StopMoving", timeToChangeLane);
		}
	}

	void StopMoving()
	{
        playerPos = transform.position;
        playerPos.x = Mathf.Round(playerPos.x);
		playerPos.y = Mathf.Round(playerPos.y);
		transform.position = playerPos;
		moving = false;
		rigid.velocity = forwardVector;
	}
}
