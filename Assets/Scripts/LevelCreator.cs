using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {

	public GameObject tilePos;
	private float startUpPosY;
	private const float tileWidth = 1.0f;
	private int heightLevel = 0;
	private GameObject tmpTile;

	private GameObject collectedTiles;
	private GameObject gameLayer;
	private GameObject bgLayer;   // bg not used yet
	private GameObject _player;

	public float gameSpeed = 4.0f;
	private float parallaxSpeed = 4.0f;
	private float outOfBounceX;
	public float outOfBounceY = -20.0f;
	private int blankCounter = 0;
	private int middleCounter = 0;
	private string lastTile = "right";
	private float startTime;
    /// <summary>
    /// Tells if game is currently in pre-start mode
    /// where it displays game logo and waits for a touch
    /// </summary>
    private bool isIdle;

	private bool enemyAdded = false;
	//private bool blobAdded = false;
	private bool playerDead = false;

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	
	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
        this.isIdle = true;

        gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");
		_player = GameObject.Find("Player");

		for (int i = 0; i<30; i++ ) 
		{
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;
			GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;
			GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			tmpG3.transform.position = Vector2.zero;
			GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}
	

		for (int i = 0; i <10; i++ )
		{
			GameObject tmpG5 = Instantiate(Resources.Load("enemy", typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = collectedTiles.transform.FindChild("killers").transform;
			tmpG5.transform.position = Vector2.zero;
		}

		for (int i = 0; i <5; i++ )
		{
			GameObject tmpG6 = Instantiate(Resources.Load("BlobJr", typeof(GameObject))) as GameObject;
			tmpG6.SetActive(false);
			tmpG6.transform.parent = collectedTiles.transform.FindChild("blobJr").transform;
			tmpG6.transform.position = Vector2.zero;
		}


		tilePos = GameObject.Find("startTilePosition");
		startUpPosY = tilePos.transform.position.y;   // grab the y coordinate from the 1st tile

		outOfBounceX = tilePos.transform.position.x -5.0f;

		fillScene(); 
		startTime = Time.time;
		collectedTiles.transform.position = new Vector2(-60.0f, -20.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (startTime - Time.time % 5 == 0)   // timer 5 em 5s
		{
			gameSpeed += 0.5f;
		}

		gameLayer.transform.position = new Vector2(gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);  //move the plataform in x position, and 0 in y poisition
		//bgLayer.transform.position = new Vector2(bgLayer.transform.position.x - parallaxSpeed * Time.deltaTime, 0); //we don hava BG assets yet
		//bgLayer.transform.position = new Vector2(bgLayer.transform.position.x + gameSpeed * Time.deltaTime, 0); // maybe we can use only the sky for this moment

		foreach(Transform child in gameLayer.transform)
		{
			if (child.position.x < outOfBounceX )   // check if some object is getting out of boundaries (left of the screen) not bellow
			{
				switch (child.gameObject.name) {
				case "ground_left(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gLeft").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
					break;
				case "ground_middle(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gMiddle").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
					break;
				case "ground_right(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gRight").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
					break;
				case "blank(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gBlank").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
					break;
				case "enemy(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild("killers").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("killers").transform;
					break;
				case "BlobJr(Clone)":
					child.gameObject.SetActive(false);
					child.gameObject.transform.position = collectedTiles.transform.FindChild("blobJr").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("blobJr").transform;
					break;
				case "Reward":
					GameObject.Find ("Reward").GetComponent<crateScript>().inPlay = false;
					break;
				default:
					Destroy(child.gameObject);  //just for the startUpTile
					break;
				}
			}
		}


		if (gameLayer.transform.childCount < 25)
			spawnTile();

		if (_player.transform.position.y < outOfBounceY)
			killPlayer();
	}

	private void fillScene()
	{
		for (int i = 0; i<15; i++ ) {
			setTile("middle");
		}
		setTile("right");
	}

	public void setTile (string type)
	{
		switch (type) {
		case "left":
			tmpTile = collectedTiles.transform.FindChild("gLeft").transform.GetChild(0).gameObject;
			break;
		case "right":
			tmpTile = collectedTiles.transform.FindChild("gRight").transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = collectedTiles.transform.FindChild("gMiddle").transform.GetChild(0).gameObject;
			break;
		case "blank":
			tmpTile = collectedTiles.transform.FindChild("gBlank").transform.GetChild(0).gameObject;
			break;
		default:
			Debug.LogError("Tile inexistente");
			break;
		}

		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector2 (tilePos.transform.position.x+(tileWidth), startUpPosY+(heightLevel * tileWidth));

		tilePos = tmpTile;
		lastTile = type;  //keep track what is the last tile used
	}

	private void spawnTile()
	{
		if (blankCounter > 0)
		{
			setTile("blank");
			blankCounter--;
			return;
		}

		if (middleCounter > 0)
		{
			randomizeEnemy();  //spawn enemies in the middle tiles
			setTile("middle");
			middleCounter--;
			return;
		}
		enemyAdded = false;


		if (lastTile == "blank")
		{
			changeHeight();
			setTile ("left");
			middleCounter = (int)Random.Range(1,8);  //change for a variable later 
		} else if (lastTile =="right") 
		{
			this.GetComponent<ScoreController>().Points ++;  //add points
			blankCounter = (int)Random.Range(1,4); //min and max gap beetween platforms
		} else if(lastTile =="middle")
		{ 
			randomizeBlob();
			setTile("right");
			//blobAdded = false;
		} 
	}


	private void changeHeight () 
	{
		int newHeightLevel = (int)Random.Range(0,4);  //probabilidade
		if (newHeightLevel < heightLevel )
			heightLevel--;
		else if (newHeightLevel > heightLevel )
			heightLevel++;
	}

	private void randomizeEnemy()
	{
		if (enemyAdded) return;

		if ((int)Random.Range(0,4) == 1 )  // "sorteio 25%
		{
			GameObject newEnemy = collectedTiles.transform.FindChild ("killers").transform.GetChild (0).gameObject;  //find the enemy in killers
			newEnemy.transform.parent = gameLayer.transform;

			newEnemy.transform.position = new Vector2(tilePos.transform.position.x+tileWidth, startUpPosY + (heightLevel* tileWidth));  //spawn at the ground
			enemyAdded = true;
		}

	}

	private void randomizeBlob()
	{
		//if (blobAdded) return;
		
		if ((int)Random.Range(0,3) == 1 )  // "sorteio 33%
		{
			GameObject newBlob = collectedTiles.transform.FindChild ("blobJr").transform.GetChild (0).gameObject;  //find the enemy in BlobJr
			newBlob.transform.parent = gameLayer.transform;
			
			newBlob.transform.position = new Vector2(tilePos.transform.position.x+tileWidth, startUpPosY + (heightLevel* tileWidth));  //spawn at the ground
			newBlob.SetActive(true);
			//blobAdded = true;
		}
		
	}

	public void killPlayer ()
	{
		if (playerDead) return;
		playerDead = true;
		this.GetComponent<ScoreController>().sendToHighscore();
		this.GetComponent<PlaySound>().PlayFX("error");

		Invoke("reloadScene", 1); //1 sec delay
	}

	public void reloadScene(){
		Application.LoadLevel(0);
	}
}
