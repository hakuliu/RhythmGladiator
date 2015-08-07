using UnityEngine;
using System.Collections;

public class StandardLaserWeapon : AbstractWeapon
{
	public float range = 100f;
	public GameObject womboAttackObj;
	
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
	private MeshRenderer womborenderer;
	private Collider wombocollider;

	protected override void Start ()
	{
		base.Start ();
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		womborenderer = womboAttackObj.GetComponent<MeshRenderer> ();
		wombocollider = womboAttackObj.GetComponent<CapsuleCollider> ();

	}

	void Update() {
		if(playervars.GlobalAttackTimer >= playervars.timeBetweenGlobalAttacks * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}
	public override void doNormalAttack ()
	{
		Shoot ();
	}

	public override void doWomboAttack ()
	{
		playervars.resetGlobalAttack ();
		wombocollider.enabled = true;
		womborenderer.enabled = true;
	}

	public void DisableEffects ()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
		wombocollider.enabled = false;
		womborenderer.enabled = false;
	}

	void OnTriggerEnter(Collider c) {
		EnemyHealth h = c.GetComponent<EnemyHealth> ();
		if (h != null) {
			h.TakeDamage(dmg, new Vector3());
		}
	}
	
	void Shoot ()
	{
		playervars.resetGlobalAttack ();
		
		gunAudio.Play ();
		
		gunLight.enabled = true;
		
		gunParticles.Stop ();
		gunParticles.Play ();
		
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (dmg, shootHit.point);
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}

