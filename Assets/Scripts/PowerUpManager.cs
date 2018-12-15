using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeRider.Assets.Classes;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {
	bool[] isCountingPower;

	public GameObject powerUpsParentUI;

    public GameObject TypingControlsImage;

    public Color PlayerBaseColor;

    public MeshRenderer PlayerMesh;

    public TrailRenderer PlayerTrail;

	private int[] powerDurations;

    GameController controller;
	PowerUpUI powerUpUI;

    Color PlayerTrailColor;

    void Awake()
    {
        controller = FindObjectOfType<GameController>();
		powerUpUI = powerUpsParentUI.GetComponent<PowerUpUI>();
        PlayerTrailColor = PlayerTrail.startColor;
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
                PlayerMesh.material.SetColor("_EmissionColor", Color.green);
                PlayerTrail.startColor = Color.green;
				isCountingPower[0] = true;
                StartCoroutine(DeactivatePowerOneCo());
                // Invoke("DeactivatePowerOne", powerDurations[0]);
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
                TypingControlsImage.GetComponent<Image>().color = Color.gray;
				isCountingPower[2] = true;
                StartCoroutine(DeactivatePowerThreeCo());
                // Invoke("DeactivatePowerThree", powerDurations[2]);
            }
        }
    }

    void DeactivatePowerOne()
    {
        CrossSceneRegistry.ActivatedPower[0] = false;
		isCountingPower[0] = false;
    }

    IEnumerator DeactivatePowerOneCo()
    {
        // Color 
        yield return new WaitForSeconds(powerDurations[0] * .8f);
        int iters = 20;
        for (int i = 0; i < iters; ++i)
        {
            if (i % 2 == 1)
            {
                PlayerMesh.material.SetColor("_EmissionColor", PlayerBaseColor);
                PlayerTrail.startColor = PlayerTrailColor;
            }
            else
            {
                PlayerMesh.material.SetColor("_EmissionColor", Color.green);
                PlayerTrail.startColor = Color.green;
            }
            yield return new WaitForSeconds((powerDurations[0] * .2f) / iters);
        }
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

    IEnumerator DeactivatePowerThreeCo()
    {
        Image img = TypingControlsImage.GetComponent<Image>();
        yield return new WaitForSeconds(powerDurations[2] * .8f);
        int iters = 20;
        for (int i = 0; i < iters; ++i)
        {
            if (i % 2 == 1)
            {
                img.color = Color.white;
            }
            else
            {
                img.color = Color.gray;
            }
            yield return new WaitForSeconds((powerDurations[2] * .2f) / iters);
        }
        controller.ToggleControls();
    }
}
