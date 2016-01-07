using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour {

	public LayerMask enemyMask;
	public float speed = 1;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth;

	// Use this for initialization
	void Start () {
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;  //get the width of the sprite

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
}
