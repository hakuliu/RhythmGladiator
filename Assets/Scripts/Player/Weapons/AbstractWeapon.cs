using UnityEngine;
using System.Collections;

public abstract class AbstractWeapon : MonoBehaviour
{
	public int dmg;
	public float standardrecoil;
	protected PlayerVars playervars;
	protected virtual void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playervars = player.GetComponent<PlayerVars> ();
	}
	public abstract void doNormalAttack();
	public abstract void doWomboAttack();
}

