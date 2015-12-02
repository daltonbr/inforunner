using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	private int _score = 0;
	private int _bestScore = 0;

	// Use this for initialization
	void Start () {
		_bestScore = getHighScoreFromDb();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.color = Color.white;
		GUIStyle _style = GUI.skin.GetStyle("Label");
		_style.alignment = TextAnchor.UpperLeft;
		_style.fontSize = 20;
		GUI.Label(new Rect (20,20,200,200), _score.ToString(),_style);
		_style.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect (Screen.width - 220,20,200,200), "Highscore: "+ _bestScore.ToString(), _style);
	}

	public int Points{
		get{return _score;}
		set{_score = value;}
	}

	static string Md5Sum(string s){
		s += GameObject.Find("xxmd5").transform.GetChild(0).name;
		System.Security.Cryptography.MD5 h = System.Security.Cryptography.MD5.Create();
		byte[] data = h.ComputeHash (System.Text.Encoding.Default.GetBytes(s));
	
			//123456

		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		for (int i = 0; i<data.Length; i++) {
			sb.Append(data[i].ToString("x2"));
		}
		return sb.ToString ();
	}

	public void saveVal(int val){
		string tmpV = Md5Sum(val.ToString() );
		PlayerPrefs.SetString("score_hash", tmpV);
		PlayerPrefs.SetInt ("score", val);
	}

	private int dec(string val){
		int tmpV = 0;
		if (val == "") {
			saveVal(tmpV);
		} else {
			if (val.Equals(Md5Sum(PlayerPrefs.GetInt("score").ToString () ))){
				tmpV = PlayerPrefs.GetInt("score");
			}else{  //corrupted DB
				saveVal (0);
			}
		}
		return tmpV;
	}


	private int getHighScoreFromDb(){
		return dec (PlayerPrefs.GetString("score_hash"));
	}

	public void sendToHighscore(){
		if (_score > _bestScore) {
			saveVal(_score);
		}

	}
}
