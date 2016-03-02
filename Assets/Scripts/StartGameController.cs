using UnityEngine;
using System.Collections;

public class StartGameController : MonoBehaviour
{

    public GameObject startWindow;
	public GameObject scorePanel;
    public GameObject[] enableOnStart;

    /// <summary>Camera animator</summary>
    private Animator animator;

    /// <summary>
    /// Tells if game is currently in pre-start mode
    /// where it displays game logo and waits for a touch
    /// </summary>
    private bool isIdle;
    /// <summary>
    /// The time scale used by the game, this is auto-assigned.
    /// </summary>
    private float gameTimeScale;

    void Awake()
    {
        this.animator = gameObject.GetComponent<Animator>();
    }
		
    void Start()
    {
        foreach (GameObject _gameObject in this.enableOnStart)
        {
            _gameObject.SetActive(false);
        }
        this.startWindow.SetActive(true);
        this.gameTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        this.isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not started
        if (this.isIdle)
        {
            // When player touches the screen to start.
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                this.startWindow.SetActive(false);
                this.animator.SetTrigger("GameStart");
				scorePanel.SetActive(true);
            }
        }
    }

    void OnGameStart()
    {
        this.animator.SetBool("Started", true);
        isIdle = false;
        foreach (GameObject _gameObject in this.enableOnStart)
        {
            _gameObject.SetActive(true);
        }
        Time.timeScale = this.gameTimeScale;
    }

}
