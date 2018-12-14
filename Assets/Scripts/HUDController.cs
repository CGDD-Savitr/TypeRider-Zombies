using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	public GameObject FlyTextObject;

	public Transform ScoreTransform;

	public Transform PowerupTransform;

	public void FlashScore(int amount)
	{
		flashScoreRelative(amount);
	}

	public void FlashPowerUp(int id)
	{
		string name = "Powerup";
		if (id == 0)
		{
			name = "Invincibility";
		}
		else if (id == 1)
		{
			name = "Slow time";
		}
		else if (id == 2)
		{
			name = "WASD controls";
		}
		flashPowerUpRelative(name, id * 100);
	}

	void flashCenter(string text)
	{
		GameObject flytext = Instantiate(FlyTextObject, Vector3.zero, Quaternion.identity);
		flytext.transform.SetParent(transform);
		flytext.transform.localPosition = new Vector2(0, -200);
		flytext.transform.localScale = Vector3.one;
		Text textObj = flytext.GetComponentInChildren<Text>();
		textObj.text = text;
	}

	void flashPowerUpRelative(string name, int offset)
	{
		Vector2 pos = Vector2.zero;
		pos.y += 50;
		pos.x += -100 + offset;
		GameObject flytext = Instantiate(FlyTextObject, Vector3.zero, Quaternion.identity);
		flytext.transform.SetParent(PowerupTransform);
		flytext.transform.localPosition = pos;
		flytext.transform.localScale = new Vector3(1, 1, 1);
		Text text = flytext.GetComponentInChildren<Text>();
		text.text = "+" + name;
	}

	void flashScoreRelative(int amount)
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
}
