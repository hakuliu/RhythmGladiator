using UnityEngine;
using System.Collections;

public class GroundSpikerAttack : AbstractEnemyAttack
{
	public GameObject projectile;
	public float beatsBetweenAttack = 4f;
	public float beatOffset;
	public float spikeTravelTime = 2f;
	float effecttimer = 0;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
	}

	protected override void assignTrack ()
	{
		BeatEvent[] events = new BeatEvent[] {
			CommonEventFactory.getDelegatedEvent (GetSpikeLambda()),
			CommonEventFactory.getNoOp ()
		};
		float[] deltas = new float[] {
			beatOffset,
			beatsBetweenAttack
		};
		track.assignEvents (events, deltas);
	}

	DelegatedBeatEvent.DelegatedAction GetSpikeLambda() {
		return delegate () {
			GameObject proj = Instantiate(projectile, this.transform.position, this.transform.rotation) as GameObject;
			AbstractProjectileScript proscript = proj.GetComponent<AbstractProjectileScript>();
			proscript.setTrackDelaysAndStart(new float[]{spikeTravelTime});
		};
	}
}

