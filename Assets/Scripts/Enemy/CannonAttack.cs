using UnityEngine;
using System.Collections;

public class CannonAttack : AbstractSingleAttack
{
	public float heightoffset = .2f;
	protected override void Start ()
	{
		base.Start ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
	}

	protected override void Attack ()
	{
		Vector3 upabit = Vector3.up;
		this.transform.TransformDirection (upabit);
		upabit = upabit.normalized * heightoffset;
		
		GameObject projectileinstance = Instantiate(projectile, this.transform.position + upabit, this.transform.rotation) as GameObject;
		AbstractProjectileScript b = projectileinstance.GetComponent<AbstractProjectileScript> ();
		b.setTrackDelaysAndStart (new float[]{4f});
	}
}

