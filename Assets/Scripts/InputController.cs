using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private bool isMobile = true;
	private PlayerController _player;  //the instance of the script that is attached to the player


	// Use this for initialization
	void Start () {
		if (Application.isEditor) isMobile = false;

		_player = GameObject.Find("Player").GetComponent<PlayerController> (); //ref for the PlayerController script
	}
	
	// Update is called once per frame
	void Update () {
		if (isMobile)
		{
			int tmpC = Input.touchCount;
			tmpC--;

			if (Input.GetTouch(tmpC).phase == TouchPhase.Began)
			{
				handlerInteraction(true);
			}

			if (Input.GetTouch(tmpC).phase == TouchPhase.Ended)
			{
				handlerInteraction(false);
			}
		}
		else   //in editor
		{
			if (Input.GetMouseButtonDown(0)){  //left mouse
				handlerInteraction(true);
			}

			if (Input.GetMouseButtonUp(0)){  //left mouse
				handlerInteraction(false);
			}
		}

	}

	void handlerInteraction(bool starting)
	{
		if (starting)
		{
			_player.jump();
		} 
		else 
		{
			_player.jumpPress = false;
		}
	}
}
