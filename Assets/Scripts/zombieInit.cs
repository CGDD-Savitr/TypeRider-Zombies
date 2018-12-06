using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieInit : MonoBehaviour {

	private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		if (anim)
		{
			anim.SetFloat("animSpeed", Random.Range(0.5f, 1.5f));
		}
	}
}
