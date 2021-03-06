﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject Player;
	private Rigidbody2D rb;
	public float thrust = 6000;
//	Animator _animator;
	Animator _attackAnimator;

	public GameObject attackAnim;
//	int jumpHash = Animator.StringToHash("Jump");
//	int runStateHash = Animator.StringToHash("Base Layer.Walk");

	public float attackCoolDown;
	public float attackTimer;

	public Collider2D attackTrigger;

//	private bool inAir = false;
	public bool jumpPress = false;
	bool isAttacking = false;

	//private int _animState = Animator.StringToHash("scratchAnimation");

	void Awake () {
		attackTrigger.enabled = false;
		rb = Player.GetComponent<Rigidbody2D>();
//		_animator = GetComponent<Animator>();
		_attackAnimator = attackAnim.GetComponent<Animator>();

		// get the length of the Scratch Animation and set the cooldown
		AnimatorStateInfo state = _attackAnimator.GetCurrentAnimatorStateInfo( 0 );
		float scratchLength = state.length;
		// Debug.Log(scratchLength);
		attackCoolDown = scratchLength;
	}

	void Update()
	{ 
		if (isAttacking)
		{
			if (attackTimer > 0)
			{
				attackTimer -= Time.deltaTime;  //decreases the timer for the next attack]
			}
			else  // time to next
			{
				isAttacking = false;
				attackTrigger.enabled = false;
				_attackAnimator.SetBool("isAttacking", false);
			}
		}
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
		if (!isAttacking )
		{
			isAttacking = true;
			attackTimer = attackCoolDown;
			attackTrigger.enabled = true;  // enables the collider2D
			_attackAnimator.SetBool("isAttacking", true);

			GameObject.Find ("Main Camera").GetComponent<PlaySound>().PlayFX("scratch");  //play a sound	
		}
	}

	public void ScalePlayer()
	{
		Debug.Log ("Scale Player");
	}
	
	}
