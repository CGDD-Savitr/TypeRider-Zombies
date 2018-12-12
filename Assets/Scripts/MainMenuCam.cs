using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour {

	Vector3 pos;

	void Start()
	{
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos.z += 0.1f * Time.deltaTime;
		transform.position = pos;
	}
}
