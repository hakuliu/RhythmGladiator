using UnityEngine;
using System.Collections;

public class AttackAction : AbstractHoldReleaseAction
{
	private PlayerVars playervars;
	//private GameObject weaponObj;
	public AttackAction(PlayerVars playervars) : base(KeyCode.Mouse0) {
		this.playervars = playervars;
	}
	protected override void doInitialAction ()
	{

	}
	protected override void doReleasedAction ()
	{

	}
	public void changeWeapon() {

	}

	void MeleeAttack() {
		playervars.resetGlobalAttack ();
		//collision.enabled = true;
		//placerenderer.enabled = true;
	}

}

