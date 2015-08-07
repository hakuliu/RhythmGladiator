using UnityEngine;
using System.Collections;

public class StandardMeleeWeapon : AbstractWeapon
{
	public float effectTime;
	public GameObject normalAttackObj;
	public GameObject womboAttackObj;
	private MeshRenderer normalrenderer;
	private Collider normalcollider;
	protected override void Start ()
	{
		base.Start ();
		normalrenderer = normalAttackObj.GetComponent<MeshRenderer> ();
		normalcollider = normalAttackObj.GetComponent<CapsuleCollider> ();
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
		normalcollider.enabled = true;
		normalrenderer.enabled = true;
	}
	void UpdateEffectsDecay() {
		if(playervars.GlobalAttackTimer >= playervars.timeBetweenGlobalAttacks * effectTime) {
			normalcollider.enabled = false;
			normalrenderer.enabled = false;
		}
	}
	void OnTriggerEnter(Collider c) {
		Debug.Log ("rawr");
		EnemyHealth h = c.GetComponent<EnemyHealth> ();
		if (h != null) {
			h.TakeDamage(dmg, new Vector3());
		}
	}
}

