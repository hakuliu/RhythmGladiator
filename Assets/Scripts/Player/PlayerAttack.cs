using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	private PlayerVars playervars;
	private AttackAction action;
	public int currentWeaponIndex = 0;
	public GameObject[] allowableWeapons;
	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playervars = player.GetComponent<PlayerVars> ();
		ScrollWeapon (0);//sets current weapon without moving weapon.
	}
	void Update() {
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll > 0) {
			ScrollWeapon(1);
		} else if (scroll < 0) {
			ScrollWeapon(-1);
		}
	}
	void FixedUpdate ()
	{
		action.FixedUpdate ();
	}

	void SetWeapon(AbstractWeapon weap) {
		//maybe cache these? idk. doesn't seem like too much overhead at the moment.
		this.action = new AttackAction(weap);
	}
	void ScrollWeapon(int dir) {
		currentWeaponIndex += dir;
		if(currentWeaponIndex < 0) {
			currentWeaponIndex = 0;
		}
		if (currentWeaponIndex >= this.allowableWeapons.Length) {
			currentWeaponIndex = this.allowableWeapons.Length - 1;
		}
		AbstractWeapon weaponToEquip = allowableWeapons [currentWeaponIndex].GetComponent<AbstractWeapon> ();
		SetWeapon (weaponToEquip);
	}
}

