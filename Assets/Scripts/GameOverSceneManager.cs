using System.Collections;
using System.Collections.Generic;
using TypeRider.Assets.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour {

	public string gameSceneName;
	public string mainMenuSceneName;

	public Text ScoreText;

	void Awake()
	{
		ScoreText.text = "SCORE: " + CrossSceneRegistry.PlayerScore;
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneName);
	}
}
