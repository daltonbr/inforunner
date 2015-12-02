using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public PlayerController _playerController;

	// Use this for initialization
	void Start () {
		//_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			GameObject tmpPlayer = GameObject.Find("Player");
			_playerController = tmpPlayer.GetComponent<PlayerController>();
			GameObject.Find ("Main Camera").GetComponent<PlaySound>().PlayFX("die");  //play a sound

			//_playerCOntroller.ScalePlayer();  // simulating a z axis movement

			tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.right*1000); //"die animation" in the player
			tmpPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.up*2000);

			// play with scale of the player to give a sensation of moving in z axis
			tmpPlayer.GetComponent<Collider2D>().enabled = false;  // disable the collider in order to sink the player
		}

	}

}
