using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				break;
			default:
				Destroy(other.gameObject);
				break;
		}
	}
}
