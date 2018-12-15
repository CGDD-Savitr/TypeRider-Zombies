using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject scoreControllerObject;
	public GameObject GameManager;
	public GameObject DamageOverlay;

	public GameObject HUDControllerObject;

	public AudioClip CoinSound;

	public AudioClip PowerupPickupSound;

    public int coinScore = 1000;

    private ScoreController scoreController;

	private HUDController hudController;

	private GameController gameController;

	Animator anim;
	AudioSource audioSource;

	private void Start()
	{
		anim = DamageOverlay.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
        scoreController = scoreControllerObject.GetComponent<ScoreController>();
		hudController = HUDControllerObject.GetComponent<HUDController>();
		gameController = GameManager.GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (GameManager)
		{
			if (other.gameObject.tag != "FloorTrigger")
			{
                switch (other.gameObject.tag)
                {
                    case "PowerUpOne":
						if (!CrossSceneRegistry.ActivatedPower[0])
							CrossSceneRegistry.CanUsePower[0] = true;
						hudController.FlashPowerUp(0);
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						Destroy(other.gameObject);
                        return;
                    case "PowerUpTwo":
						if (!CrossSceneRegistry.ActivatedPower[1])
							CrossSceneRegistry.CanUsePower[1] = true;
						hudController.FlashPowerUp(1);
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						Destroy(other.gameObject);
                        return;
                    case "PowerUpThree":
						if (!CrossSceneRegistry.ActivatedPower[2])
							CrossSceneRegistry.CanUsePower[2] = true;
						hudController.FlashPowerUp(2);
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						Destroy(other.gameObject);
                        return;
                    case "Coin":
                        scoreController.AddScore(coinScore);
						hudController.FlashScore(coinScore);
						if (audioSource && CoinSound)
							audioSource.PlayOneShot(CoinSound);
                        Destroy(other.gameObject);
                        return;
                }

                if (CrossSceneRegistry.ActivatedPower[0] == false){
					if (gameController.TakeDamage())
					{
						if (anim)
						{
							anim.SetTrigger("TakeDamage");
						}
						if (audioSource)
						{
							audioSource.Play();
						}
					}
                }
                
			}
		}
	} 
}
