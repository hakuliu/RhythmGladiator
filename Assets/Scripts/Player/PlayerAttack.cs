using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	private PlayerVars playervars;
	private AttackAction action;
	public GameObject[] allowableWeapons;
	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playervars = player.GetComponent<PlayerVars> ();
		GameObject defaultWeapon = allowableWeapons [0];
		AbstractWeapon defaultWeaponScript = defaultWeapon.GetComponent<AbstractWeapon> ();
		action = new AttackAction (defaultWeaponScript);
	}

	void FixedUpdate ()
	{
		action.FixedUpdate ();
	}


}

