using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFlytext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(DestroySelf());
	}

	IEnumerator DestroySelf()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
