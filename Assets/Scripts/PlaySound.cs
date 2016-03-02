using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	private AudioSource[] _audioSource;
	bool isMute;

	// Use this for initialization
	void Awake () {
		_audioSource = this.GetComponents<AudioSource> ();
	//	_audioListener = this.GetComponent<AudioListener> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayFX(string type){
		switch (type){
		case "die":
			_audioSource[0].Play ();
			break;
		case "jump":
			_audioSource[1].Play ();
			break;
		case "power":
			_audioSource[2].Play ();
			break;
		case "error":
			_audioSource[3].Play ();
			break;
		case "scratch":
			_audioSource[4].Play ();
			break;
		case "blobDie":
			_audioSource[5].Play ();
			break;

		}
	}
		
	public void Mute ()
	{
		isMute = ! isMute;
		AudioListener.volume = isMute ? 0 : 1;  //AudioListener is static
	}
}
