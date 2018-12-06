using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

	public GameObject GameManager;

	void OnTriggerEnter(Collider other)
	{
		if (GameManager)
		{
			if (other.gameObject.tag != "Table")
			{
				GameManager.SendMessage("TakeDamage");
			}
		}
	}
}
