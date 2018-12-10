using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidRotate : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
	}
}
