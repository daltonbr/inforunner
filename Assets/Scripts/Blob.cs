using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour {

	public LayerMask enemyMask;
	public float speed = 1;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth;

	public PlayerController _playerController;

	private GameObject collectedTiles;

	// Use this for initialization
	void Start () {
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;  //get the width of the sprite

	}

	// Use this for initialization
	void Awake () {
		_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		collectedTiles = GameObject.Find("tiles");
	}


	// Update is called once per frame
	void FixedUpdate () {
		// Always move forward (left)
		Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.left);
		//myBody.AddForce(Vector2.left * speed * Time.deltaTime);

		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
		
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			_playerController.Die();
		}
	}

	public void KillEnemy()
	{
		this.gameObject.SetActive(false);
		this.gameObject.transform.position = collectedTiles.transform.FindChild("blobJr").transform.position;
		this.gameObject.transform.parent = collectedTiles.transform.FindChild("blobJr").transform;

		//playsounds
		//flip
		//jump
	}
}