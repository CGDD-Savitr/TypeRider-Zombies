using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TypeRider.Assets.Classes;

public class PowerUpUI : MonoBehaviour {

	GameObject[] powers;
	GameObject[] sliderParents;
	Slider[] sliders;
	int[] powerDurations;

	// Use this for initialization
	void Start () {
		powers = new GameObject[3];
		sliderParents = new GameObject[3];
		sliders = new Slider[3];
		powerDurations = new int[3];

		powers[0] = transform.Find("PowerOne").gameObject;
		powers[1] = transform.Find("PowerTwo").gameObject;
		powers[2] = transform.Find("PowerThree").gameObject;
		sliderParents[0] = powers[0].transform.Find("Slider").gameObject;
		sliderParents[1] = powers[1].transform.Find("Slider").gameObject;
		sliderParents[2] = powers[2].transform.Find("Slider").gameObject;
		sliders[0] = sliderParents[0].GetComponent<Slider>();
		sliders[1] = sliderParents[1].GetComponent<Slider>();
		sliders[2] = sliderParents[2].GetComponent<Slider>();

		powerDurations = CrossSceneRegistry.PowerDurations;

		for (int i = 0; i < 3; i++)
		{
			sliders[i].maxValue = powerDurations[i];
			sliders[i].value = powerDurations[i];
			sliderParents[i].SetActive(false);
		}

		foreach (GameObject power in powers)
		{
			power.SetActive(false);
		}
	}

	void Update()
	{
		for (int i = 0; i < 3; i++)
		{
			if (!powers[i].activeSelf && CrossSceneRegistry.CanUsePower[i])
			{
				powers[i].SetActive(true);
				sliders[i].value = powerDurations[i];
			}
		}

		for (int i = 0; i < 3; i++)
		{
			if (sliderParents[i].activeSelf == true)
			{
				sliders[i].value -= Time.deltaTime;
				if (sliders[i].value <= 0f)
				{
					sliderParents[i].SetActive(false);
					powers[i].SetActive(false);
				}
			}
		}
	}

	public void ActivatePower(int powerIndex)
	{
		if (powerIndex >= 0 && powerIndex <= 2)
		{
			sliderParents[powerIndex].SetActive(true);
		}
	}
}
