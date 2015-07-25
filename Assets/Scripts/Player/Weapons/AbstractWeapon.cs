using UnityEngine;
using System.Collections;

public class AbstractWeapon
{
	private int dmg;
	private float standardrecoil;

	public abstract void doNormalAttack();
	public abstract void doWomboAttack();
}

