using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject Player;
	public Rigidbody2D rb;
	public float thrust = 6000;
	Animator _animator;
	Animator _attackAnimator;

	public GameObject attackAnim;
	int jumpHash = Animator.StringToHash("Jump");
	int runStateHash = Animator.StringToHash("Base Layer.Walk");
	public float attackCoolDown = 0.5f;

	private bool inAir = false;
	public bool jumpPress = false;
	bool isAttacking = false;

	//private int _animState = Animator.StringToHash("animState");

	//public float speed = 0.1F;

	void Awake () {
		rb = Player.GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_attackAnimator = attackAnim.GetComponent<Animator>();
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

	public void Die() {
		GameObject.Find ("Main Camera").GetComponent<PlaySound>().PlayFX("die");  //play a sound
		
		//_playerCOntroller.ScalePlayer();  // simulating a z axis movement
		
		this.GetComponent<Rigidbody2D>().AddForce(Vector2.right*1000); //"die animation" in the player
		this.GetComponent<Rigidbody2D>().AddForce(Vector2.up*2000);
		
		// play with scale of the player to give a sensation of moving in z axis
		this.GetComponent<Collider2D>().enabled = false;  // disable the collider in order to sink the player
	}

	public void Attack()
	{	
		if (isAttacking)
		{
			isAttacking = false;
			attackAnim.SetActive(false);

		}
		else  //attack
		{
			isAttacking = true;
			attackAnim.SetActive(true);
			AnimatorStateInfo attackStateInfo = _attackAnimator.GetCurrentAnimatorStateInfo(0);  //0 is the base layer of the animator
			Debug.Log ("Player Attacks!");
		}
	}

	public void ScalePlayer()
	{
		Debug.Log ("Scale Player");
	}
	
	}
