using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

	public GameObject GameManager;
	public GameObject DamageOverlay;

	Animator anim;
	AudioSource audioSource;

	private void Start()
	{
		anim = DamageOverlay.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
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
						{
							CrossSceneRegistry.CanUsePower[0] = true;
							Destroy(other.gameObject);
						}
                        return;
                    case "PowerUpTwo":
						if (!CrossSceneRegistry.ActivatedPower[1])
						{
							CrossSceneRegistry.CanUsePower[1] = true;
							Destroy(other.gameObject);
						}
                        return;
                    case "PowerUpThree":
						if (!CrossSceneRegistry.ActivatedPower[2])
						{
							CrossSceneRegistry.CanUsePower[2] = true;
							Destroy(other.gameObject);
						}
                        return;
                }

                if (CrossSceneRegistry.ActivatedPower[0] == false){
                    GameManager.SendMessage("TakeDamage");
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
