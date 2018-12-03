using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float initialForwardVelocity = 0.5f;
	public float laneChangeVelocity = 2f;

	private float timeToChangeLane;

	private bool moving = false;

	private Rigidbody rigid;

	private Vector3 changeLaneVectorLeft;
	private Vector3 changeLaneVectorRight;
	private Vector3 forwardVector;
    private Vector3 playerPos;

    private int laneNumber = 1;

	void Start ()
	{
		rigid = GetComponent<Rigidbody>();
		timeToChangeLane = 1 / laneChangeVelocity;
		forwardVector = new Vector3(0f, 0f, initialForwardVelocity);
		changeLaneVectorLeft = new Vector3(-laneChangeVelocity, 0f, initialForwardVelocity);
		changeLaneVectorRight = new Vector3(laneChangeVelocity, 0f, initialForwardVelocity);
		rigid.velocity = forwardVector;
        playerPos = new Vector3(0f, 0f, 0f);
	}
	
	void Update () {
		if (!moving)
		{
			if (Input.GetKey(KeyCode.A))
			{
				if (laneNumber > 0)
				{
					rigid.velocity = changeLaneVectorLeft;
					moving = true;
					Invoke("StopMoving", timeToChangeLane);
				}
			}
			else if (Input.GetKey(KeyCode.D))
			{
				if (laneNumber < 2)
				{
					rigid.velocity = changeLaneVectorRight;
					moving = true;
					Invoke("StopMoving", timeToChangeLane);
				}
			}
		}
	}

	void StopMoving()
	{
        playerPos = transform.position;
        playerPos.x = Mathf.Round(playerPos.x);
        transform.position = playerPos;
		moving = false;
		rigid.velocity = forwardVector;
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "LeftLane":
				laneNumber = 0;
				break;
			case "MiddleLane":
				laneNumber = 1;
				break;
			case "RightLane":
				laneNumber = 2;
				break;
		}
	}
}
