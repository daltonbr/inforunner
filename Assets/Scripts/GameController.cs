using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool isPaused = false;

	public void TooglePauseButton () {   // Toogle Pause states
		if (isPaused) Unpause();
		else Pause ();
	}

	void Pause () {
		Time.timeScale = 0.0f;
		isPaused = true;
	}

	void Unpause(){
		Time.timeScale = 1.0f;
		isPaused = false;
	}

}
