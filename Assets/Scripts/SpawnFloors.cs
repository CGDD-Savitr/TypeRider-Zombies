using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloors : MonoBehaviour {

	public Transform floorParent;
	public GameObject[] floorPrefabs;
	public float floorLength = 40f;

	private Vector3 otherFloor;
	private bool spaceFloorWasLast = false;
	private int rand;

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				otherFloor = other.transform.parent.position;
				rand = (int)Random.Range(0f, 2.99f);

				if (spaceFloorWasLast)
				{
					Instantiate(
						floorPrefabs[1],
						new Vector3(otherFloor.x, otherFloor.y, otherFloor.z + floorLength),
						Quaternion.identity, floorParent
					);
					spaceFloorWasLast = false;
				}
				else
				{
					Instantiate(
						floorPrefabs[rand],
						new Vector3(otherFloor.x, otherFloor.y, otherFloor.z + floorLength),
						Quaternion.identity, floorParent
					);

					if (rand == 2)
					{
						spaceFloorWasLast = true;
					}

				}
				
				break;
		}
	}
}
