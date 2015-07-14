using UnityEngine;
using System.Collections;

public class PlayerMelee : MonoBehaviour
{
	public int damage;
	public float effectTime;
	private PlayerVars globalvars;
	private BoxCollider collision;
	private MeshRenderer placerenderer;

	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		globalvars = player.GetComponent<PlayerVars> ();
		collision = GetComponent<BoxCollider> ();
		placerenderer = GetComponent<MeshRenderer> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Fire2") && globalvars.CanAttack()) {
			MeleeAttack();
		}
		UpdateEffectsDecay ();
	}
	void MeleeAttack() {
		globalvars.resetGlobalAttack ();
		collision.enabled = true;
		placerenderer.enabled = true;
	}
	void UpdateEffectsDecay() {
		if(globalvars.GlobalAttackTimer >= globalvars.timeBetweenGlobalAttacks * effectTime) {
			collision.enabled = false;
			placerenderer.enabled = false;
		}
	}
	void OnTriggerEnter(Collider c) {
		EnemyHealth h = c.GetComponent<EnemyHealth> ();
		if (h != null) {
			h.TakeDamage(damage, new Vector3());
		}
	}
}

