using UnityEngine;
using System.Collections;

public class StandardMeleeWeapon : AbstractWeapon
{
	public float effectTime;
	
	private BoxCollider collision;
	private MeshRenderer placerenderer;
	protected override void Start ()
	{
		base.Start ();
		collision = GetComponent<BoxCollider> ();
		placerenderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateEffectsDecay ();
	}

	public override void doNormalAttack ()
	{
		MeleeAttack ();
	}

	public override void doWomboAttack ()
	{

	}
	void MeleeAttack() {
		playervars.resetGlobalAttack ();
		collision.enabled = true;
		placerenderer.enabled = true;
	}
	void UpdateEffectsDecay() {
		if(playervars.GlobalAttackTimer >= playervars.timeBetweenGlobalAttacks * effectTime) {
			collision.enabled = false;
			placerenderer.enabled = false;
		}
	}
	void OnTriggerEnter(Collider c) {
		EnemyHealth h = c.GetComponent<EnemyHealth> ();
		if (h != null) {
			h.TakeDamage(dmg, new Vector3());
		}
	}
}

