using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	private AudioSource[] _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = this.GetComponents<AudioSource> ();
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


		}
	}
}
