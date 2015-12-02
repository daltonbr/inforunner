using UnityEngine;
using System.Collections;

public class crateScript : MonoBehaviour {

	private float minY, maxY;
	private float direction = 1;

	public bool inPlay = true;
	private bool releaseCrate = false;

	private SpriteRenderer crateRenderer;


	// Use this for initialization
	void Start () {
		maxY = this.transform.position.y + .5f;   //hardcoded...for now?
		minY = maxY - 1.0f;

		crateRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + (Mathf.Sin (direction * 0.05f)) );
		if (this.transform.position.y > maxY )
			direction = -1;
		if (this.transform.position.y < minY )
			direction = 1;

		if (!inPlay  && !releaseCrate) 
			respawn ();
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" )
		{
			switch (crateRenderer.sprite.name) {
			case "exclamationBox_1":
				GameObject.Find ("Main Camera").GetComponent<LevelCreator>().gameSpeed -= 1.0f;
				break;
			case "exclamationBox_2":
				GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(Vector2.up*6000);
				break;
			case "exclamationBox_3":  // more points
				Debug.Log ("Player coletou: " + crateRenderer.sprite.name );
				GameObject.Find("Main Camera").GetComponent<ScoreController>().Points +=10; 
				break;
			case "exclamationBox_4":
				// ?
				Debug.Log ("Player coletou: " + crateRenderer.sprite.name );
				break;
			}

			inPlay = false;
			//move the collected crate out of the scene =)
			this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 30.0f); //moving up
			GameObject.Find ("Main Camera").GetComponent<PlaySound>().PlayFX("power");  //play a sound
		}
	}

	void respawn ()
	{
		releaseCrate = true;
		Invoke ("placeCrate", (float)Random.Range (3,10));  /// the next crate will be spwaned in 3 to 10 secs
	}

	void placeCrate() 
	{
		inPlay = true;
		releaseCrate = false;

		GameObject tmpTile = GameObject.Find("Main Camera").GetComponent<LevelCreator>().tilePos;
		this.transform.position = new Vector2(tmpTile.transform.position.x, tmpTile.transform.position.y + 3.0f);
		maxY = this.transform.position.y + .5f;   //hardcoded...for now?
		minY = maxY - 1.0f;
	}
}
