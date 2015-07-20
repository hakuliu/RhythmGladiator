using UnityEngine;
using System.Collections;

public class GroundSpikerAttack : MonoBehaviour
{
	public GameObject projectile;
	public float beatsBetweenAttack = 4f;
	public float beatOffset;
	public float spikeTravelTime = 2f;
	BeatTracker track;
	GameObject player;
	float effecttimer = 0;
	DamageManager damager;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		track = new BeatTracker ();
		PopulateTrack ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
	}
	void FixedUpdate ()
	{
		track.FixedUpdate ();
	}

	void PopulateTrack() {
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

