using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloors : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "FloorTrigger":
				Destroy(other.transform.parent.gameObject);
				break;
		}
	}
}
