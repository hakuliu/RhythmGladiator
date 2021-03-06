using UnityEngine;
using System.Collections;

public class BearAttack : AbstractSingleAttack
{
	public float beatsTilDetonation = 4f;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
	}

	protected override void Attack ()
	{
		audioSource.Play ();
		Vector3 upabit = Vector3.up;
		this.transform.TransformDirection (upabit);
		upabit *= 1.3f;

		GameObject projectileinstance = Instantiate(projectile, this.transform.position + upabit, this.transform.rotation) as GameObject;
		AbstractProjectileScript b = projectileinstance.GetComponent<AbstractProjectileScript> ();
		b.setTrackDelaysAndStart (new float[]{beatsTilDetonation});
		anim.SetTrigger("Fire");
	}
}

