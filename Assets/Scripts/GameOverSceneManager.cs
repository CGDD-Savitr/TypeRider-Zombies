using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour {

	public string gameSceneName;
	public string mainMenuSceneName;

	public void PlayGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneName);
	}
}
