using UnityEngine;
using System.Collections;

public class ExitGateScript : MonoBehaviour
{
	public bool letPlayerWin = false;
	private EnemyManager em;
	private GameObject player;
	// Use this for initialization
	void Start ()
	{
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		em = managers.GetComponent<EnemyManager> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnTriggerEnter(Collider col) {
		if(col == this.player.GetComponent<CapsuleCollider>()) {
			if(letPlayerWin) {
				LoadNextLevel();
			}
		}
	}

	public void allowWin() {
		letPlayerWin = true;
	}
	private void LoadNextLevel() {
		int i = Application.loadedLevel;
		Application.LoadLevel(i+1);
	}
}

