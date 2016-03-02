using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
	
	public bool isPaused = false;
	private GameObject pauseMenu;
	private GameObject jumpButton;
	private GameObject attackButton;

	void Awake() {
		pauseMenu = GameObject.Find("PauseMenu");
		jumpButton = GameObject.Find("JumpButton");
		attackButton = GameObject.Find("AttackButton");
		if (!pauseMenu) Debug.LogError("GameObject PauseMenu not found");
		HidePauseMenu();
	}
	
	public void TooglePauseButton () {   // Toogle Pause states
		if (isPaused) Unpause();
		else Pause ();
	}
	
	public void Pause () {
		Time.timeScale = 0.0f;
		isPaused = true;
		ShowPauseMenu();
	}
	
	public void Unpause(){
		Time.timeScale = 1.0f;
		isPaused = false;
		HidePauseMenu();
	}

	public void RestartLevel() {
		//Application.LoadLevel(Application.loadedLevel);  // ** deprecated

		// get the current scene name 
		string sceneName = SceneManager.GetActiveScene().name;

		// load the same scene
		SceneManager.LoadScene(sceneName,LoadSceneMode.Single);

		Unpause();
	}

	public void Quit() {
		Application.Quit();
	}
	
	public void ShowPauseMenu() {
		pauseMenu.SetActive(true);
		jumpButton.SetActive(false);
		attackButton.SetActive(false);
	}
	
	public void HidePauseMenu() {
		pauseMenu.SetActive(false);
		jumpButton.SetActive(true);
		attackButton.SetActive(true);
	}


}
