using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

	public GameObject GameManager;
	public GameObject DamageOverlay;

	Animator anim;

	private void Start()
	{
		anim = DamageOverlay.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (GameManager)
		{
			if (other.gameObject.tag != "Table")
			{
				GameManager.SendMessage("TakeDamage");
				if (anim)
				{
					anim.SetTrigger("TakeDamage");
				}
			}
		}
	}
}
