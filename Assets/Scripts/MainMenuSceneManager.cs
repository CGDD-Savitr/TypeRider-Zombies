using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{

	public string gameSceneName;

	public void PlayGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
