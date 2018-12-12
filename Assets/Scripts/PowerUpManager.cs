using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeRider.Assets.Classes;

public class PowerUpManager : MonoBehaviour {
	bool[] isCountingPower;

	public GameObject powerUpsParentUI;

	private int[] powerDurations;

    GameController controller;
	PowerUpUI powerUpUI;

    void Awake()
    {
        controller = FindObjectOfType<GameController>();
		powerUpUI = powerUpsParentUI.GetComponent<PowerUpUI>();
	}
	// Use this for initialization
	void Start () {
		isCountingPower = new bool[3];

		powerDurations = CrossSceneRegistry.PowerDurations;

		CrossSceneRegistry.ActivatedPower = new bool[3];
		CrossSceneRegistry.CanUsePower = new bool[3];
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossSceneRegistry.ActivatedPower[0] == true)
        {
            if (!isCountingPower[0])
            {
				//done in playerCollisions script
				powerUpUI.ActivatePower(0);
				isCountingPower[0] = true;
                Invoke("DeactivatePowerOne", powerDurations[0]);
            }
        }
        if (CrossSceneRegistry.ActivatedPower[1] == true)
        {
            if (!isCountingPower[1])
            {
				powerUpUI.ActivatePower(1);
				Time.timeScale *= 0.5f;
				isCountingPower[1] = true;
                Invoke("DeactivatePowerTwo", powerDurations[1]);
            }
        }
        if (CrossSceneRegistry.ActivatedPower[2] == true)
        {
            if (!isCountingPower[2])
            {
				powerUpUI.ActivatePower(2);
				controller.ToggleControls();
				isCountingPower[2] = true;
                Invoke("DeactivatePowerThree", powerDurations[2]);
            }
        }
    }

    void DeactivatePowerOne()
    {
        CrossSceneRegistry.ActivatedPower[0] = false;
		isCountingPower[0] = false;
    }

    void DeactivatePowerTwo()
    {
        Time.timeScale *= 2;
        CrossSceneRegistry.ActivatedPower[1] = false;
		isCountingPower[1] = false;
    }

    void DeactivatePowerThree()
    {
        controller.ToggleControls();
        CrossSceneRegistry.ActivatedPower[2] = false;
		isCountingPower[2] = false;
    }
}
