using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	public GameObject FlyTextObject;

	public Transform ScoreTransform;

	public void FlashScore(int amount)
	{
		Vector2 pos = Vector2.zero;
		pos.y += 40;
		pos.x += 110;
		GameObject flytext = Instantiate(FlyTextObject, Vector3.zero, Quaternion.identity);
		flytext.transform.SetParent(ScoreTransform);
		flytext.transform.localPosition = pos;
		flytext.transform.localScale = new Vector3(1, 1, 1);
		Text text = flytext.GetComponentInChildren<Text>();
		text.text = "+" + amount;
	}

	public void FlashPowerUp(int id)
	{
		
	}

	void Flash(string text, Vector2 position)
	{
		FlyTextObject.transform.localPosition = position;
		FlyTextObject.SetActive(true);
		// Fly
		// flyText.text = text;
		// StartCoroutine(AnimateFlyText());
	}
}
