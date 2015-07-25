using UnityEngine;
using System.Collections;

public class AttackAction : AbstractHoldReleaseAction
{
	private PlayerVars playervars;
	private AbstractWeapon weapon;
	//private GameObject weaponObj;
	public AttackAction(AbstractWeapon weapon) : base(KeyCode.Mouse0) {
		this.weapon = weapon;
	}
	protected override void doInitialAction ()
	{
		weapon.doNormalAttack ();
	}
	protected override void doReleasedAction ()
	{
		weapon.doWomboAttack ();
	}
	public void changeWeapon(AbstractWeapon weapon) {
		this.weapon = weapon;
	}

	void MeleeAttack() {
		playervars.resetGlobalAttack ();
		//collision.enabled = true;
		//placerenderer.enabled = true;
	}

}

