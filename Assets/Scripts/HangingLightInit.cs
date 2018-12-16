using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLightInit : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.enabled = false;
		Invoke("StartAnim", Random.Range(0f, 2f));
	}

	void StartAnim()
	{
		anim.enabled = true;
	}
}
