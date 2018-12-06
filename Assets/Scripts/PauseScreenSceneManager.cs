using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenSceneManager : MonoBehaviour {
	
	public string mainMenuSceneName;
	
	public void MainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneName);
	}
}
