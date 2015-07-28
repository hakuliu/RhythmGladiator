using UnityEngine;
using System.Collections;

public class GroundSpikerAttack : AbstractSingleAttack
{
	public float spikeTravelTime = 2f;
	float effecttimer = 0;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
	}

	protected override void Attack ()
	{
		GameObject proj = Instantiate(projectile, this.transform.position, this.transform.rotation) as GameObject;
		AbstractProjectileScript proscript = proj.GetComponent<AbstractProjectileScript>();
		proscript.setTrackDelaysAndStart(new float[]{spikeTravelTime});
	}
}

