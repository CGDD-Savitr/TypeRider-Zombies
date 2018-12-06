using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	public string gameOverSceneName;
	public string mainMenuSceneName;
	public string pauseScreenSceneName;

	public void GameOver()
	{
		SceneManager.LoadScene(gameOverSceneName);
	}

	public void Pause()
	{
		SceneManager.LoadScene(pauseScreenSceneName, LoadSceneMode.Additive);
	}

	public void Unpause()
	{
		SceneManager.UnloadSceneAsync(pauseScreenSceneName);
	}
}
