using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloors : MonoBehaviour {

	public Transform floorParent;
	public GameObject floorPrefab;
	public float floorLength = 40f;

	private Vector3 otherFloor;

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				otherFloor = other.transform.parent.position;
				Instantiate(
					floorPrefab,
					new Vector3(otherFloor.x, otherFloor.y, otherFloor.z + floorLength),
					Quaternion.identity, floorParent
					);
				break;
		}
	}
}
