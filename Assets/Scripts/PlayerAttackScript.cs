using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour {

	private Blob _blobScript;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)  //"Attackable" layer
		{
			Debug.Log("Hit "+ other.gameObject.name);
			_blobScript = other.gameObject.GetComponent<Blob>();  //reference for the enemy that will be kille
			_blobScript.KillEnemy();
		}
	}

}
