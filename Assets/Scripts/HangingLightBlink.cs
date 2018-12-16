using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLightBlink : MonoBehaviour {

	private MeshRenderer mesh;
	private Color color;
	private bool waiting = false;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer>();
		color = mesh.material.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!waiting)
		{
			waiting = true;
			Invoke("Blink", Random.Range(0.1f, 3f));
		}
	}

	void Blink()
	{
		mesh.material.SetColor("_EmissionColor", color * Random.Range(0f, 1f));
		waiting = false;
	}
}
