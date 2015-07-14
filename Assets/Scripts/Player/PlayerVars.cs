using UnityEngine;
using System.Collections;
/// <summary>
/// doesn't do anything but keep a bunch of states and variables for the player.
/// I do this so every other player script has easy way to access common things.
/// </summary>
public class PlayerVars : MonoBehaviour
{
	public float timeBetweenGlobalAttacks;
	///to be replaced later by types of sword but for now do this.
	public bool hasSword;
	public bool hasGun;


	///shared among shooting and melee (and other future things?)
	private float globalAttackTimer;
	public float GlobalAttackTimer {
		get {
			return globalAttackTimer;
		}
	}
	public void resetGlobalAttack() {
		this.globalAttackTimer = 0;
	}
	public bool CanAttack() {
		return globalAttackTimer >= timeBetweenGlobalAttacks;
	}

	/// <summary>
	/// update timers if necessary.
	/// </summary>
	void FixedUpdate() {
		this.globalAttackTimer += Time.fixedDeltaTime;
	}
}
