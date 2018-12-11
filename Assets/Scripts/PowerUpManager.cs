using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeRider.Assets.Classes;

public class PowerUpManager : MonoBehaviour {
    bool isCountingPowerOne;
    bool isCountingPowerTwo;
    bool isCountingPowerThree;
    public int powerOneDuration = 5;
    public int powerTwoDuration = 5;
    public int powerThreeDuration = 10;

    GameController controller;

    void Awake()
    {
        controller = FindObjectOfType<GameController>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossSceneRegistry.ActivatedPower[0] == true)
        {
            if (!isCountingPowerOne)
            {
                //done in playerCollisions script
                isCountingPowerOne = true;
                Invoke("DeactivatePowerOne", powerOneDuration);
            }
        }
        if (CrossSceneRegistry.ActivatedPower[1] == true)
        {
            if (!isCountingPowerTwo)
            {
                Time.timeScale *= 0.5f;
                isCountingPowerTwo = true;
                Invoke("DeactivatePowerTwo", powerTwoDuration);
            }
        }
        if (CrossSceneRegistry.ActivatedPower[2] == true)
        {
            if (!isCountingPowerThree)
            {
                controller.ToggleControls();
                isCountingPowerThree = true;
                Invoke("DeactivatePowerThree", powerThreeDuration);
            }
        }
    }

    void DeactivatePowerOne()
    {
        CrossSceneRegistry.ActivatedPower[0] = false;
        isCountingPowerOne = false;
    }

    void DeactivatePowerTwo()
    {
        Time.timeScale *= 2;
        CrossSceneRegistry.ActivatedPower[1] = false;
        isCountingPowerTwo = false;
    }

    void DeactivatePowerThree()
    {
        controller.ToggleControls();
        CrossSceneRegistry.ActivatedPower[2] = false;
        isCountingPowerThree = false;
    }
}
