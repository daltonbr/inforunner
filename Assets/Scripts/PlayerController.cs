using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject Player;
	public Rigidbody2D rb;
	public float thrust = 6000;
	Animator _animator;
	int jumpHash = Animator.StringToHash("Jump");
	int runStateHash = Animator.StringToHash("Base Layer.Walk");

	private bool inAir = false;
	public bool jumpPress = false;

	//private int _animState = Animator.StringToHash("animState");

	//public float speed = 0.1F;

	void Start () {
		rb = Player.GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	void Update() { 

		/*
		if (!inAir && Mathf.Abs(rb.velocity.y) > 0.05f )  //abs handles negative y - falling down
		{
			_animator.SetBool("Jump", true);
			inAir = true;
		} else if (inAir && rb.velocity.y == 0.0f) 
		{
			_animator.SetBool("Jump", false);
			inAir = false;
			if (jumpPress) jump(); 
		}

		*/

		//int nbTouches = Input.touchCount;
		//float move = Input.GetAxis ("Vertical");

		AnimatorStateInfo stateinfo = _animator.GetCurrentAnimatorStateInfo(0);  //0 is the base layer of the animator
		/*
		if ( (Input.touchCount > 0) || (Input.GetKeyDown(KeyCode.Space)) ) { // && Input.GetTouch(0).phase == TouchPhase.Moved) {
			//&& stateInfo.nameHash = runStateHash) //veifia 
			jump ();
			anim.SetBool("Jump", true); //ineficiente pq tem que evaluate a string, com hash id eh melhor
			//anim.SetTrigger (jumpHash);

		}
		else {
			anim.SetBool("Jump", false);
		}
		*/
		}

	public void jump()
	{
		jumpPress = true;
		//if (inAir) return;
		rb.AddForce(Vector2.up * thrust); //else: jump
		GameObject.Find ("Main Camera").GetComponent<PlaySound>().PlayFX("jump");  //play a sound
	}

	public void ScalePlayer()
	{
		Debug.Log ("Scale Player");
	}
	
	}
