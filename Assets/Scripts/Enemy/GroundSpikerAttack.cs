using UnityEngine;
using System.Collections;

public class GroundSpikerAttack : MonoBehaviour
{
	public GameObject projectile;
	public float beatsBetweenAttack = 4f;
	public float beatOffset;
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
	
	// Update is called once per frame
	void Update ()
	{
	
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

		};
	}
}

