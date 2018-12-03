using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;
	public float followDistance = 2f;
	private Vector3 followVector;
	
	void Start()
	{
		followVector = new Vector3(transform.position.x, transform.position.y, -followDistance);
	}

	void Update () {
		followVector.z = toFollow.position.z - followDistance;
		transform.position = followVector;
	}
}
