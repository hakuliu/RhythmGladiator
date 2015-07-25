using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	private PlayerVars playervars;
	private AttackAction action;
	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playervars = player.GetComponent<PlayerVars> ();

		action = new AttackAction (playervars);
	
	}

	void FixedUpdate ()
	{
	
	}
}

