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

	public GameObject PowerupParticlePrefab;

	public GameObject CoinParticlePrefab;

	public AudioClip CoinSound;

	public AudioClip PowerupPickupSound;

	public int powerupScore = 1000;

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
						if (CrossSceneRegistry.ActivatedPower[0] || CrossSceneRegistry.CanUsePower[0])
						{
							scoreController.AddScore(powerupScore);
							hudController.FlashScore(powerupScore);
						}
						if (!CrossSceneRegistry.ActivatedPower[0])
						{
							if (!CrossSceneRegistry.CanUsePower[0])
								hudController.FlashPowerUp(0);
							CrossSceneRegistry.CanUsePower[0] = true;
						}
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						if (PowerupParticlePrefab)
							Instantiate(PowerupParticlePrefab, other.transform.position, Quaternion.identity);
						Destroy(other.gameObject);
                        return;
                    case "PowerUpTwo":
						if (CrossSceneRegistry.ActivatedPower[1] || CrossSceneRegistry.CanUsePower[1])
						{
							scoreController.AddScore(powerupScore);
							hudController.FlashScore(powerupScore);
						}
						if (!CrossSceneRegistry.ActivatedPower[1])
						{
							if (!CrossSceneRegistry.CanUsePower[1])
								hudController.FlashPowerUp(1);
							CrossSceneRegistry.CanUsePower[1] = true;
						}
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						if (PowerupParticlePrefab)
							Instantiate(PowerupParticlePrefab, other.transform.position, Quaternion.identity);
						Destroy(other.gameObject);
                        return;
                    case "PowerUpThree":
						if (CrossSceneRegistry.ActivatedPower[2] || CrossSceneRegistry.CanUsePower[2])
						{
							scoreController.AddScore(powerupScore);
							hudController.FlashScore(powerupScore);
						}
						if (!CrossSceneRegistry.ActivatedPower[2])
						{
							if (!CrossSceneRegistry.CanUsePower[2])
								hudController.FlashPowerUp(2);
							CrossSceneRegistry.CanUsePower[2] = true;
						}
						if (audioSource && PowerupPickupSound)
							audioSource.PlayOneShot(PowerupPickupSound);
						if (PowerupParticlePrefab)
							Instantiate(PowerupParticlePrefab, other.transform.position, Quaternion.identity);
						Destroy(other.gameObject);
                        return;
                    case "Coin":
                        scoreController.AddScore(coinScore);
						hudController.FlashScore(coinScore);
						if (audioSource && CoinSound)
							audioSource.PlayOneShot(CoinSound);
						if (CoinParticlePrefab)
							Instantiate(CoinParticlePrefab, other.transform.position, Quaternion.identity);
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
