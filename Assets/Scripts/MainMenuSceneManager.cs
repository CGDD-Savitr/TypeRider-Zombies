using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{

	public string gameSceneName;

	void Start()
	{
		Time.timeScale = 1.0f;
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
